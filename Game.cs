using System;

namespace blackjack {

    public class Game {

        private Deck deck;
        private Player player;
        private Player dealer;

        public Game() {
            deck = new Deck();
            player = new Player();
            dealer = new Player();
        }

        public void Start() {
            ResetGame();
            deck.Shuffle();
            InitialDeal();
            PlayerTurn();
            DealerTurn();
            DetermineWinner();
        }

        private void ResetGame()
        {
            player.Reset();
            dealer.Reset();
            deck = new Deck(); // Create a new deck for each game
        }

        private void DisplayHands(bool hideFirstDealerCard = false)
        {
            List<string> playerDisplay = player.GetHandDisplay();
            List<string> dealerDisplay = dealer.GetHandDisplay(hideFirstDealerCard);

            Console.WriteLine("Player's Hand:                    Dealer's Hand:");
            
            int maxLines = Math.Max(playerDisplay.Count, dealerDisplay.Count);
            for (int i = 0; i < maxLines; i++)
            {
                string playerLine = i < playerDisplay.Count ? playerDisplay[i] : new string(' ', 30);
                string dealerLine = i < dealerDisplay.Count ? dealerDisplay[i] : "";
                
                ConsoleHelper.WriteColor(playerLine, ConsoleColor.White);
                Console.Write("    ");
                ConsoleHelper.WriteColor(dealerLine, hideFirstDealerCard && i == 0 ? ConsoleColor.DarkGray : ConsoleColor.White);
                Console.WriteLine();
            }

            if (!hideFirstDealerCard)
            {
                ConsoleHelper.WriteLineColor($"Player Score: {player.Score}", ConsoleColor.Yellow);
                ConsoleHelper.WriteLineColor($"Dealer Score: {dealer.Score}", ConsoleColor.Yellow);
            }
            else
            {
                ConsoleHelper.WriteLineColor($"Player Score: {player.Score}", ConsoleColor.Yellow);
            }
        }

        private void InitialDeal() {
            player.AddCardToHand(deck.DrawCard());
            dealer.AddCardToHand(deck.DrawCard());
            player.AddCardToHand(deck.DrawCard());
            dealer.AddCardToHand(deck.DrawCard());

            DisplayHands(true);
        }

        private void PlayerTurn() {
            while (true) {
                ConsoleHelper.WriteLineColor("\nDo you want to (H)it or (S)tand", ConsoleColor.Cyan);
                string? input = Console.ReadLine();

                if (input == null) {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                string choice = input.ToUpper();

                if (choice == "H") {
                    Card drawnCard = deck.DrawCard();
                    ConsoleHelper.WriteLineColor($"\nYou drew: {drawnCard}", ConsoleColor.Green);
                    player.AddCardToHand(drawnCard);
                    DisplayHands(true);

                    if (player.Score > 21) {
                        ConsoleHelper.WriteLineColor("Bust!", ConsoleColor.Red);
                        return;
                    }
                }
                else if (choice == "S") {
                    break;
                }
            }
        }

        private void DealerTurn() {
            ConsoleHelper.WriteLineColor("\nDealer's turn.", ConsoleColor.Cyan);
            DisplayHands();

            while (dealer.Score < 17) {
                Card drawnCard = deck.DrawCard();
                ConsoleHelper.WriteLineColor($"\nDealer drew: {drawnCard}", ConsoleColor.Green);
                dealer.AddCardToHand(drawnCard);
                DisplayHands();
            }

            if (dealer.Score > 21) {
                ConsoleHelper.WriteLineColor("Dealer busts!", ConsoleColor.Red);
            }
        }

        private void DetermineWinner() {
            ConsoleColor resultColor = ConsoleColor.Yellow;
            string result;

            if (player.Score > 21) {
                result = "\nYou lose.";
                resultColor = ConsoleColor.Red;
            }
            else if (dealer.Score > 21 || player.Score > dealer.Score) {
                result = "\nYou win!";
                resultColor = ConsoleColor.Green;
            }
            else if (player.Score == dealer.Score) {
                result = "\nIt's a push!";
            }
            else {
                result = "\nDealer wins.";
                resultColor = ConsoleColor.Red;
            }

            ConsoleHelper.WriteLineColor(result, resultColor);
            PromptToRestartOrQuit();
        }

        private void PromptToRestartOrQuit() {
            Console.WriteLine("\nDo you want to (P)lay again or (Q)uit?");
            string? input = Console.ReadLine();

            if (input == null) {
                Console.WriteLine("Invalid input. Please try again.");
                PromptToRestartOrQuit(); // Recursively call the method if input is invalid
                return;
            }

            string choice = input.ToUpper();

            if (choice == "P") {
                Console.Clear();
                Start(); // This will now reset the game properly
            }
            else if (choice == "Q") {
                Console.WriteLine("Thanks for playing!");
                Environment.Exit(0);
            }
            else {
                Console.WriteLine("Invalid input. Please try again.");
                PromptToRestartOrQuit(); // Recursively call the method if input is invalid
            }
        }
    }
}