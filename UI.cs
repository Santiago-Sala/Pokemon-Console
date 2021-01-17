using System;

namespace PokemonConsole
{
    class UI
    {
        internal void Message(string pMessage)
        {
            
            Console.WriteLine("***********************************************");
            Console.WriteLine();
            Console.WriteLine(pMessage);
            Console.WriteLine();
            Console.WriteLine("***********************************************");
            Console.WriteLine();
            
        }

        //Validating pokeSlot
        protected int ValidateSlot(string pMessage)
        {
            Console.Write(pMessage);
            string stringOutput = Console.ReadLine();

            bool isValidInt = int.TryParse(stringOutput, out int output);

            if (output < 1 || output > 6)
            {
                isValidInt = false;
            }

            while (!isValidInt)
            {

                Console.Write("Please enter a slot between 1-6. ([1], [2]), [3], [4], [5] or [6]). ");
                stringOutput = Console.ReadLine();
                isValidInt = int.TryParse(stringOutput, out output);

                if (output < 1 || output > 6)
                {
                    isValidInt = false;
                }
            }

            return output;
        }

        //Validating input for type of pokemon
        protected string ValidatePokemon(string pMessage)
        {
            Console.Write(pMessage);
            string output = Console.ReadLine().ToLower();

            bool correct = true;
            while (correct)
            {
                if (output == "f" || output == "w" || output == "g")
                {
                    correct = false;

                    switch (output)
                    {
                        case "f":
                            output = "fire";
                            break;
                        case "w":
                            output = "water";
                            break;
                        case "g":
                            output = "grass";
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Could not understand your answer.");
                    Console.Write("Please type \"f\" for Fire type pokemon, \"w\" for Water type pokemon or \"g\" for grass type pokemon. ");
                    output = Console.ReadLine().ToLower();
                }
            }

            return output;
        }

        //Validating answer Yes or No
        protected string YesOrNo(string pAnswer)
        {
            Console.Write(pAnswer);
            string output = Console.ReadLine().ToLower();

            bool correct = true;
            while (correct)
            {
                if (output == "y" || output == "n")
                {
                    correct = false;
                }
                else
                {
                    Console.WriteLine("Could not understand your answer.");
                    Console.Write("Please type \"y\" for yes and \"n\" for no. ");
                    output = Console.ReadLine().ToLower();
                }
            }

            return output;
        }

        //Getting a number from a range
        protected static int Randomise(int pFrom, int pTo)
        {
            Random dice = new Random();
            int output = dice.Next(pFrom, pTo);
            return output;
        }
                
        internal void EnterTallGrass(BackPack pBackPack)
        {
            int turnsBeforeFinish = Randomise(3, 11);
            int currentTurn = 1;

            do
            {
                bool allPokemonFainted = CheckPokemonsHP(pBackPack);
                if (allPokemonFainted)
                {
                    Console.WriteLine("All your pokemons have fainted. You return to your hometown. GAME OVER!!");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine($"************ {currentTurn} / {turnsBeforeFinish} *************");  
                Console.WriteLine("Theres tall grass in front of you. ");
                string answer = YesOrNo("Do you want to enter the tall grass? ([Y]es or [N]o) ");

                if (answer == "y")
                {
                    Pokemon randomPokemon = new Pokemon(Randomise(3, 11));

                    Console.WriteLine("You encountered a:");
                    randomPokemon.PrintNameLevelHP();

                    BattleMenu(pBackPack, randomPokemon);

                    currentTurn++;
                }
                else
                {
                    Console.WriteLine("You decided to continue your journey to Olot City and not encounter any pokemons.");
                    currentTurn++;
                }
            } while (currentTurn <= turnsBeforeFinish);
        }

        private bool CheckPokemonsHP(BackPack pBackPack)
        {
            bool output = false;
            int counter = 0;
            int ammountPokemons = pBackPack.pokemonBag.Length;

            foreach (var pokemon in pBackPack.pokemonBag)
            {
                try
                {
                    if (pokemon.CurrentHP <= 0)
                    {
                        counter++;
                    }
                }
                catch (NullReferenceException)
                {
                    counter++;
                }
                
            }

            if (counter == ammountPokemons)
            {
                output = true;
            }

            return output;
            
        }

        private void BattleMenu(BackPack pBackPack, Pokemon pRandomPokemon)
        {
            
            bool allMyPokemonFainted = CheckPokemonsHP(pBackPack);
            
            while (!allMyPokemonFainted && pRandomPokemon.CurrentHP > 0)
            {
                Console.WriteLine();

                Console.WriteLine("What do you want to do next?");

                string move = CheckMove("[A]ttack, [U]se an Item, [C]hange pokemon or [R]un. ");

                if (move == "a")
                {
                    bool firstToAttack = CheckSpeed(pBackPack, pRandomPokemon);

                    if (firstToAttack)
                    {
                        MyPokemonAttacks(pBackPack, pRandomPokemon);

                        if (pRandomPokemon.CurrentHP > 0)
                        {
                            for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
                            {

                                if (pBackPack.pokemonBag[i].CurrentHP > 0)
                                {
                                    Console.WriteLine($"{pRandomPokemon.Name} attacks!");
                                    bool hit = HitOrMiss();

                                    if (hit)
                                    {
                                        RandomPokemonAttacks(pBackPack.pokemonBag[i], pRandomPokemon);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{pRandomPokemon.Name} missed!!");
                                        break;
                                    }
                                }
                            }
                        }
                        
                    }
                    else //Need to check what happens when my pokemon faints!!
                    {
                        for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
                        {

                            if (pBackPack.pokemonBag[i].CurrentHP > 0)
                            {
                                Console.WriteLine($"{pRandomPokemon.Name} attacks!");
                                bool hit = HitOrMiss();

                                if (hit)
                                {
                                    RandomPokemonAttacks(pBackPack.pokemonBag[i], pRandomPokemon);

                                    if (pBackPack.pokemonBag[i].CurrentHP > 0)
                                    {
                                        MyPokemonAttacks(pBackPack, pRandomPokemon);
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"{pRandomPokemon.Name} missed!!");
                                    break;
                                }
                            }
                        }
                        
                    }
                }
                else if (move == "u")
                {
                    bool caughtPokemon = false;
                    CheckItems(pBackPack, pRandomPokemon, ref caughtPokemon);

                    if (caughtPokemon)
                    {
                        break;
                    }
                }
                else if (move == "c")
                {
                    ChangePokemon(pBackPack);

                    for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
                    {

                        if (pBackPack.pokemonBag[i].CurrentHP > 0)
                        {
                            Console.WriteLine($"{pRandomPokemon.Name} attacks!");
                            bool hit = HitOrMiss();

                            if (hit)
                            {
                                RandomPokemonAttacks(pBackPack.pokemonBag[i], pRandomPokemon);
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"{pRandomPokemon.Name} missed!!");
                                break;
                            }
                        }
                    }


                }
                //Run
                else
                {
                    bool canFlee = Run();

                    if (canFlee)
                    {
                        Console.WriteLine("You managed to run away from battle!!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You failed to run away.");
                    }
                }
                
            }
        }


        //********** MENU **********
        //Checking what to do next
        private string CheckMove(string pMessage)
        {
            Console.Write(pMessage);
            string output = Console.ReadLine().ToLower();

            bool correct = true;
            while (correct)
            {
                if (output == "a" || output == "u" || output == "c" || output == "r")
                {
                    correct = false;
                }
                else
                {
                    Console.WriteLine("Could not understand your answer.");
                    Console.Write("Please type \"a\" to Attack, \"u\" to use one of your items, \"c\" to change your current pokemon or \"r\" to try to flee from battle. ");
                    output = Console.ReadLine().ToLower();
                }
            }

            return output;
        }



        //********** ATTACKING **********
        //Random pokemon attacks
        private void RandomPokemonAttacks(Pokemon pPokemon, Pokemon pRandomPokemon)
        {
            int attackEffectiveness = CheckEffectiveness(pRandomPokemon, pPokemon);
            int damage = pRandomPokemon.Attack * attackEffectiveness;
            Console.WriteLine($"{pPokemon.Name} took -{damage} of damage.");
            pPokemon.CurrentHP -= damage;

            if (pPokemon.CurrentHP <= 0)
            {
                pPokemon.CurrentHP = 0;
                Console.WriteLine($"{pPokemon.Name} fainted.");
            }
            else
            {
                Console.WriteLine($"{pPokemon.Name} HP is {pPokemon.CurrentHP} / {pPokemon.FullHP}");
            }

        }

        //Avatar pokemon attacks
        private void MyPokemonAttacks(BackPack pBackPack, Pokemon pRandomPokemon)
        {
            for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
            {
                if (pBackPack.pokemonBag[i].CurrentHP > 0)
                {
                    Console.WriteLine($"{pBackPack.pokemonBag[i].Name} attacks!");
                    bool hit = HitOrMiss();

                    if (hit)
                    {
                        int attackEffectiveness = CheckEffectiveness(pBackPack.pokemonBag[i], pRandomPokemon);
                        int damage = pBackPack.pokemonBag[i].Attack * attackEffectiveness;
                        Console.WriteLine($"{pRandomPokemon.Name} took -{damage} of damage.");
                        pRandomPokemon.CurrentHP -= damage;

                        if (pRandomPokemon.CurrentHP <= 0)
                        {
                            pRandomPokemon.CurrentHP = 0;
                            Console.WriteLine($"{pRandomPokemon.Name} fainted.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{pRandomPokemon.Name} HP is {pRandomPokemon.CurrentHP} / {pRandomPokemon.FullHP}");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{pBackPack.pokemonBag[i].Name} missed!!");
                        break;
                    }
                }
            }
        }

        //Checking for effectiveness via types.
        private int CheckEffectiveness(Pokemon pAttackerPokemon, Pokemon pDefendingPokemon)
        {
            int output = 1;

            if (pAttackerPokemon.Type.ToLower() == "fire")
            {
                switch (pDefendingPokemon.Type.ToLower())
                {
                    case "water":
                        output = 1;
                        Console.WriteLine($"{pAttackerPokemon.Name}'s attack was NOT very effective towards {pDefendingPokemon.Name}!!");
                        break;
                    case "grass":
                        output = 2;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was super effective towards  {pDefendingPokemon.Name}!!");
                        break;
                    case "fire":
                        output = 1;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was OK towards  {pDefendingPokemon.Name}!!");
                        break;
                }
            }
            else if (pAttackerPokemon.Type.ToLower() == "water")
            {
                switch (pDefendingPokemon.Type.ToLower())
                {
                    case "water":
                        output = 1;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was OK towards  {pDefendingPokemon.Name}!!");
                        break;
                    case "grass":
                        output = 1;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was NOT very effective towards  {pDefendingPokemon.Name}!!");
                        break;
                    case "fire":
                        output = 2;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was super effective towards  {pDefendingPokemon.Name}!!");
                        break;
                }
            }
            else
            {
                switch (pDefendingPokemon.Type.ToLower())
                {
                    case "water":
                        output = 2;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was OK towards  {pDefendingPokemon.Name}!!");
                        break;
                    case "grass":
                        output = 1;
                        Console.WriteLine("The pokemon attack!");
                        break;
                    case "fire":
                        output = 1;
                        Console.WriteLine($"{ pAttackerPokemon.Name}'s attack was NOT very effective towards  {pDefendingPokemon.Name}!!");
                        break;
                }
            }

            return output;
        }

        //Hit or miss attack
        private bool HitOrMiss()
        {
            bool output = true;
            int firstDice = Randomise(1, 51);
            int secondDice = Randomise(1, 51);

            if (firstDice < secondDice)
            {
                output = false;
            }
            else
            {
                output = true;
            }

            return output;
        }

        //Checking who is attacking first
        private bool CheckSpeed(BackPack pBackPack, Pokemon pRandomPokemon)
        {
            bool output = true;


            for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
            {
                if (pBackPack.pokemonBag[i].CurrentHP > 0)
                {
                    if (pBackPack.pokemonBag[i].Speed >= pRandomPokemon.Speed)
                    {
                        //My pokemon attacks first!!
                        output = true;
                        break;
                    }                    
                    else
                    {
                        //Enemy attacks first
                        output = false;
                        break;
                    }
                }
            }


            return output;
        }


        //********** ITEMS **********
        //Checking contents of the backpack
        private void CheckItems(BackPack pBackPack, Pokemon pRandomPokemon, ref bool pCaughtPokemon)
        {
            pBackPack.CheckPotions();
            pBackPack.CheckPokeballs();

            string move = UseItems();

            if (move == "k")
            {
                int ammountPokeballs = pBackPack.AmmountPokeballs(pBackPack);
                if (ammountPokeballs == 0)
                {
                    Console.Write("You do not have any pokeballs left!!");
                    Console.WriteLine("You missed your turn!! XD");
                }
                else
                {
                    Console.WriteLine("You threw a pokeball!!");
                    UsePokeball(pBackPack, pRandomPokemon, ref pCaughtPokemon);
                }

            }
            else
            {
                int ammountPotions = pBackPack.AmmountPotions(pBackPack);
                if (ammountPotions == 0)
                {
                    Console.Write("You do not have any potions left!!");
                    Console.WriteLine("You missed your turn!! XD");
                }
                else
                {
                    UsePotion(pBackPack);
                }
                
            }
        }

        //Using a potion
        private void UsePotion(BackPack pBackPack)
        {
            int counter = 0;
            foreach (var pokemon in pBackPack.pokemonBag)
            {
                try
                {
                    counter++;
                    Console.WriteLine();
                    Console.WriteLine($"********* Slot - {counter} *********");
                    pokemon.PrintNameLevelHP();
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Empty - No pokemon here.");
                }
                
            }

            int slot = ValidateSlot("In which slot is the pokemon you want to use the potion to ? ");

            if (pBackPack.pokemonBag[slot -1] == null)
            {
                Console.WriteLine("Told you no pokemon in this slot. You lost your turn and a potion. XD");

                //Removing a potion from backpack
                for (int i = 0; i < pBackPack.itemsBag[1].Length; i++)
                {
                    if (pBackPack.itemsBag[1][i] != null)
                    {
                        pBackPack.itemsBag[1][i] = null;
                        break;
                    }
                }
            }
            else if (pBackPack.pokemonBag[slot -1].CurrentHP == 0)
            {
                Console.WriteLine("Sorry but potions can not revive. This time you only lose your turn. XD");

            }
            else if (pBackPack.pokemonBag[slot -1].CurrentHP == pBackPack.pokemonBag[slot - 1].FullHP)
            {
                Console.WriteLine("Wanted more life than you should have, huh? Being an smart ass makes you lose your turn and a potion. XD");

                //Removing a potion from backpack
                for (int i = 0; i < pBackPack.itemsBag[1].Length; i++)
                {
                    if (pBackPack.itemsBag[1][i] != null)
                    {
                        pBackPack.itemsBag[1][i] = null;
                        break;
                    }
                }
            }
            else
            {
                pBackPack.pokemonBag[slot - 1].CurrentHP += 20;

                for (int i = 0; i < pBackPack.itemsBag[1].Length; i++)
                {
                    if (pBackPack.itemsBag[1][i] != null)
                    {
                        pBackPack.itemsBag[1][i] = null;
                        break;
                    }
                }

                if (pBackPack.pokemonBag[slot -1 ].CurrentHP > pBackPack.pokemonBag[slot - 1].FullHP)
                {
                    pBackPack.pokemonBag[slot - 1].CurrentHP = pBackPack.pokemonBag[slot -1 ].FullHP;
                    Console.WriteLine($"{pBackPack.pokemonBag[slot -1 ].Name} recovered full health, {pBackPack.pokemonBag[slot - 1].CurrentHP} / {pBackPack.pokemonBag[slot -1 ].FullHP}.");
                }
                else
                {
                    Console.WriteLine($"{pBackPack.pokemonBag[slot - 1].Name} current health is {pBackPack.pokemonBag[slot - 1].CurrentHP} / {pBackPack.pokemonBag[slot -1 ].FullHP}");
                }

            }

            pBackPack.CheckPotions();
        }

        //Using pokeball
        private void UsePokeball(BackPack pBackPack, Pokemon pRandomPokemon, ref bool pCaughtPokemon)
        {
            bool catchPokemon = pBackPack.ThrowPokeball(pBackPack);

            if (catchPokemon)
            {
                Console.WriteLine($"You have catch: {pRandomPokemon.Name}.");

                pCaughtPokemon = true;

                string answer = YesOrNo($"Do you want to put a nickname to {pRandomPokemon.Name}? ");

                if (answer == "y")
                {
                    pRandomPokemon.ChangeName();
                }
                
                pBackPack.AddPokemon(pRandomPokemon);
            }
            else
            {
                Console.WriteLine($"{pRandomPokemon.Name} scape from the ball");

                for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
                {

                    if (pBackPack.pokemonBag[i].CurrentHP > 0)
                    {
                        Console.WriteLine($"{pRandomPokemon.Name} attacks!");
                        bool hit = HitOrMiss();

                        if (hit)
                        {
                            RandomPokemonAttacks(pBackPack.pokemonBag[i], pRandomPokemon);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{pRandomPokemon.Name} missed!!");
                            break;
                        }
                    }
                }
            }
        }

        //Verifying what item to use
        private string UseItems()
        {
            Console.Write("Press [K] to use a pokeball or [P] to use a potion. ");
            string output = Console.ReadLine().ToLower();

            bool correct = true;
            while (correct)
            {
                if (output == "k" || output == "p")
                {
                    correct = false;
                }
                else
                {
                    Console.WriteLine("Could not understand your answer.");
                    Console.Write("Please type \"k\" to use a pokeball or \"p\" to use a potion. ");
                    output = Console.ReadLine().ToLower();
                }
            }
            return output;
        }


        //********** CHANGING **********
        private void ChangePokemon(BackPack pBackPack)
        {
            int counter = 0;
            foreach (var pokemon in pBackPack.pokemonBag)
            {
                try
                {
                    counter++;
                    Console.WriteLine();
                    Console.WriteLine($"********* Slot - {counter} *********");
                    pokemon.PrintNameLevelHP();
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Empty - No pokemon here.");
                }

            }

            int slot = ValidateSlot("In which slot is the pokemon you want to use? ");

            Pokemon tempPokemon = new Pokemon();

            for (int i = 0; i < pBackPack.pokemonBag.Length; i++)
            {

                if (pBackPack.pokemonBag[i].CurrentHP > 0)
                {
                    tempPokemon = pBackPack.pokemonBag[i];
                    break;
                }
            }
            if (pBackPack.pokemonBag[slot - 1] == null)
            {
                Console.WriteLine("Sorry no pokemons there. You lose your turn. XD");
            }
            else
            {
                pBackPack.pokemonBag[0] = pBackPack.pokemonBag[slot - 1];
                pBackPack.pokemonBag[slot - 1] = tempPokemon;

                Console.WriteLine($"You change your pokemon to {pBackPack.pokemonBag[0].Name}.");
                Console.WriteLine($"You put {pBackPack.pokemonBag[slot - 1].Name} to your bag. Slot {slot}.");
            }
            
        }


        //********** RUNNING **********
        //Try to flee
        private bool Run()
        {
            string answer = YesOrNo("Are you sure you want to flee? ");
            bool output = true;
            int dice = Randomise(1, 6);

            if (answer == "y")
            {
                switch (dice)
                {
                    case 3:
                        output = false;
                        break;
                    case 4:
                        output = false;
                        break;
                    default:
                        output = true;
                        break;
                }
            }
            
            return output;
        }
    }
}
