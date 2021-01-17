using System;

namespace PokemonConsole
{

    class BackPack : UI
    {
        public Pokemon[] pokemonBag { get; set; }
        public Items[][] itemsBag { get; set; }

        public BackPack()
        {
            pokemonBag = new Pokemon[6];
            itemsBag = new Items[2][];
            itemsBag[0] = new Pokeball[10];
            itemsBag[1] = new Potion[20];
        }

        internal void CheckPokemonsStats()
        {
            int counter = 1;

            foreach (var pokemon in pokemonBag)
            {
                try
                {
                    Console.WriteLine($"********** Slot: {counter} **********");
                    pokemon.PrintStats();
                    counter++;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Slot is empty");
                    counter++;
                    Console.WriteLine();
                }

            }
        }

        internal void AddPokemon(Pokemon pPokemon)
        {
            bool pokeBagIsFull = true;
            for (int i = 0; i < 6; i++)
            {
                if (pokemonBag[i] == null)
                {
                    pokemonBag[i] = pPokemon;
                    Console.WriteLine($"{pPokemon.Name} was added to your team.");
                    pokeBagIsFull = false;
                    break;
                }                
            }

            if (pokeBagIsFull)
            {
                Console.WriteLine("Your pokebag is full. ");
                string answer = YesOrNo("Do you want to remove a pokemon from your bag? ([Y]es or [N]o). ");

                if (answer == "y")
                {
                    CheckPokebag();
                    Console.WriteLine();
                    int pokePosition = ValidateSlot("Which pokemon do you want to remove? (Slot 1-6). ");
                    Console.WriteLine($"Goodbye {pokemonBag[pokePosition - 1].Name}");
                    pokemonBag[pokePosition - 1] = null;                    
                    pokemonBag[pokePosition - 1] = pPokemon;
                    Console.WriteLine($"You added {pPokemon.Name} in slot {pokePosition}");
                }

            }
        }

        internal void CheckPokebag()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"********** Slot: {i + 1} **********");
                
                try
                {
                    Console.WriteLine($"Name: {pokemonBag[i].Name}");
                    Console.WriteLine($"Level: {pokemonBag[i].Level}");
                    Console.WriteLine($"HP: {pokemonBag[i].CurrentHP} / {pokemonBag[i].FullHP}");
                    Console.WriteLine();
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Slot is empty");
                    Console.WriteLine();
                }
            };
        }

        internal void CheckPokeballs()
        {
            int counter = 0;
            foreach (var item in itemsBag[0])
            {
                if (item != null)
                {
                    counter++;
                }
            }

            Console.WriteLine($"You have {counter} / {itemsBag[0].Length} pokeballs.");
        }

        internal void CheckPotions()
        {
            int counter = 0;
            foreach (var item in itemsBag[1])
            {
                if (item != null)
                {
                    counter++;
                }
            }

            Console.WriteLine($"You have {counter} / {itemsBag[1].Length} potions.");
        }

        internal void AddItem(Items[] pSlot, Items pItem, int pAmmount)
        {
            int counter = pAmmount;

            while (counter > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (pSlot[i] == null)
                    {
                        pSlot[i] = pItem;
                        counter--;
                    }
                    if (counter == 0)
                    {
                        Console.WriteLine($"You added {pAmmount} x {pItem.Name} in your backpack.");
                        break;
                    }
                    if (counter > 0 && i == 9)
                    {
                        Console.WriteLine($"Your backpack is full!!");
                        counter = 0;
                        break;
                    }
                }
            }

        }

        internal int AmmountPokeballs(BackPack pBackPack)
        {
            int counter = 0;
            foreach (var item in pBackPack.itemsBag[0])
            {
                if (item != null)
                {
                    counter++;
                }
            }

            return counter;
        }

        internal bool ThrowPokeball(BackPack pBackPack)
        {
            bool output;
            int dice = Randomise(1, 4);

            switch (dice)
            {
                case 1:
                    output = true;                    
                    break;
                default:
                    output = false;
                    break;
            }

            //Removing a pokeball from the items bag
            for (int i = 0; i < pBackPack.itemsBag[0].Length; i++)
            {
                if (pBackPack.itemsBag[0][i] != null)
                {
                    pBackPack.itemsBag[0][i] = null;
                    break;
                }
            }
            return output;
        }

        internal int AmmountPotions(BackPack pBackPack)
        {
            int counter = 0;
            foreach (var item in pBackPack.itemsBag[1])
            {
                if (item != null)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
