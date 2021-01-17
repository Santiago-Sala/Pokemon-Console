using System;

namespace PokemonConsole
{
    class Program
    {
        static void Main(string[] args)
        {            
            UI helper = new UI();

            //Welcome
            helper.Message("Welcome to pokemon console edition.");

            Avatar avatar = new Avatar();
            Console.Write("What is your name? ");
            avatar.Name = Console.ReadLine();
            Console.WriteLine($"{avatar.Name} let your journey begin!!");

            //Choosing first pokemon
            BackPack backPack = new BackPack();
            //Adding Pokemon
            backPack.AddPokemon(avatar.ChoosePokemon());

            backPack.CheckPokebag();
            backPack.pokemonBag[0].PrintStats();
            
            
            //Journey Starts
            Console.WriteLine("Now that you acquiured your pokemon lets begin your journey.");
            Console.WriteLine("But first you will need this items:");

            //Receiving potions and pokeballs and adding them to the backpack.
            Pokeball pokeball = new Pokeball();
            Potion potion = new Potion();
            backPack.AddItem(backPack.itemsBag[0], pokeball, 5);
            backPack.AddItem(backPack.itemsBag[1], potion, 5);
            backPack.CheckPokeballs();
            backPack.CheckPotions();

            Console.WriteLine("Let your journey begin");
            Console.WriteLine("You decided to leave your hometown and head to Olot City");
            Console.WriteLine();

            //Road to Olot
            helper.EnterTallGrass(backPack);


            //Heippa
            helper.Message("Thank you playing");


            Console.ReadLine();
        }

    }
}
