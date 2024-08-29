using System;

namespace blackjack {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("\nWelcome to Blackjack! Created by Darius Molina.\n");

            // initialize game
            Game game = new Game();
            game.Start();
        }
    }
}
