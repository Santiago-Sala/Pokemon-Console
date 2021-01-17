using System;


namespace PokemonConsole
{
   

    class Avatar : UI
    {
        public string Name { get; set; }

       

        public Avatar()
        {
            Name = "";
            
        }

        //Choosing your first pokemon.
        internal Pokemon ChoosePokemon()
        {
            Pokemon output;

            Console.WriteLine("So you become of age. Now its time for you to have your own Pokémon.");
            string answer = YesOrNo("Do you want your first pokemon to be random? ([Y]es or [N]o). ");

            if (answer == "y")
            {
                output = new Pokemon();
                Console.WriteLine($"Congratulations you received {output.Name}. This are his stats:");
                output.PrintStats();

                NickName(output);
                
            }
            else
            {
                string type = ValidatePokemon("Do you want a [F]ire type, a [W]ater type  or a [G]rass type pokemon? ");
                output = new Pokemon(type);
                Console.WriteLine($"Congratulations you received {output.Name}. This are his stats:");
                output.PrintStats();

                NickName(output);
            }

            return output;
        }

        //Changing pokemons Nickname
        private void NickName(Pokemon pPokemon)
        {
            string changeName = YesOrNo($"Do you want to put a nickname to {pPokemon.Name}? ([Y]es or [N]o). ");
            if (changeName == "y")
            {
                bool correct = true;
                while (correct)
                {
                    pPokemon.ChangeName();
                    string newAnswer = YesOrNo("Are you happy with this change? ([Y]es or [N]o) ");
                    if (newAnswer == "y")
                    {
                        correct = false;
                    }
                }

            }
        }

       
    }
}
