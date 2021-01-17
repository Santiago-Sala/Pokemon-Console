using System;

namespace PokemonConsole
{
    class Pokemon : UI
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int FullHP { get; set; }
        public int CurrentHP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }        

        //Choosing randomly between one of the 3 starting pokemons.
        public Pokemon()
        {
            Type = RandomType();
            Name = ChaSquBul(Type);
            Level = 5;
            FullHP = 30;
            CurrentHP = 30;
            Attack = 5;
            Defence = 5;
            Speed = 5;
        }

        //Choosing the type of your first pokemon
        public Pokemon(string pType)
        {
            Type = pType;
            Name = ChaSquBul(Type);
            Level = 5;
            FullHP = 30;
            CurrentHP = 30;
            Attack = 5;
            Defence = 5;
            Speed = 5;

        }

        //Random encounter of pokemon
        public Pokemon(int pLevel)
        {
            Type = RandomType();
            Name = ChaSquBul(Type);
            Level = pLevel;
            FullHP = RandomHP(pLevel);
            CurrentHP = FullHP;
            Attack = RandomStat(pLevel);
            Defence = RandomStat(pLevel);
            Speed = RandomStat(pLevel);
        }

        //Randomizing attack, defence and speed stats depending on level
        private int RandomStat(int pLevel)
        {
            int output = 0;

            switch (pLevel)
            {
                case 3:
                    output = Randomise(1, 6);
                    break;
                case 4:
                    output = Randomise(3, 9);
                    break;
                case 5:
                    output = Randomise(5, 11);
                    break;
                case 6:
                    output = Randomise(6, 13);
                    break;
                case 7:
                    output = Randomise(7, 15);
                    break;
                case 8:
                    output = Randomise(8, 17);
                    break;
                case 9:
                    output = Randomise(9, 19);
                    break;
                case 10:
                    output = Randomise(10, 22);
                    break;
            }

            return output;
        }

        //Randomizing HP for random encounter
        private int RandomHP(int pLevel)
        {
            int output = 0;

            switch (pLevel)
            {
                case 3:
                    output = Randomise(25, 31);
                    break;
                case 4:
                    output = Randomise(27, 34);
                    break;
                case 5:
                    output = Randomise(29, 37);
                    break;
                case 6:
                    output = Randomise(31, 40);
                    break;
                case 7:
                    output = Randomise(33, 43);
                    break;
                case 8:
                    output = Randomise(35, 46);
                    break;
                case 9:
                    output = Randomise(37, 49);
                    break;
                case 10:
                    output = Randomise(39, 52);
                    break;
            }

            return output;
        }

        //Changing pokemons name
        internal void ChangeName()
        {
            Console.WriteLine("What is his new nickname?");
            Name = Console.ReadLine();
            Console.WriteLine($"His new name is {Name}");
        }

        //Printing stats
        internal void PrintStats()
        {
            Console.WriteLine();
            Console.WriteLine("***********************");
            Console.WriteLine($"* Name:    {Name}");
            Console.WriteLine($"* Type:    {Type}");
            Console.WriteLine($"* Level:   {Level}");
            Console.WriteLine($"* Hp:      {CurrentHP} / {FullHP}");
            Console.WriteLine($"* Attack:  {Attack}");
            Console.WriteLine($"* Defence: {Defence}");
            Console.WriteLine($"* Speed:   {Speed}");
            Console.WriteLine("***********************");
            Console.WriteLine();
        }

        //Depending of the random type you get a pokemon. 
        private string ChaSquBul(string pType)
        {
            string output = "";

            if (pType.ToLower() == "fire")
            {
                output = "Charmander";
            }
            else if (pType.ToLower() == "water")
            {
                output = "Squirtle";
            }
            else if (pType.ToLower() == "grass")
            {
                output = "Bulbasaur";
            }
            return output;
        }

        //Randomizing the type of the pokemon
        private string RandomType()
        {
            string output = "";
            int type = Randomise(0, 3);

            switch (type)
            {
                case 0:
                    output = "Fire";
                    break;
                case 1:
                    output = "Water";
                    break;
                case 2:
                    output = "Grass";
                    break;
            }


            return output;
        }

        internal void PrintNameLevelHP()
        {
            Console.WriteLine();
            Console.WriteLine($"******** {Name} ********");
            Console.WriteLine($"*** HP: {CurrentHP} / {FullHP}");
            Console.WriteLine($"*** Level: {Level}");
            Console.WriteLine("***************************");
        }

    }
}
