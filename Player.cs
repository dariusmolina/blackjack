using System;
using System.Collections.Generic;

namespace blackjack {

    public class Player {
        public List<Card> Hand {get; private set;}
        public int Score => CalculateScore();

        public Player() {
            Hand = new List<Card>();
        }

        public void AddCardToHand(Card card) {
            Hand.Add(card);
        }

        private int CalculateScore() {
            int score = 0;
            int aceCount = 0;

            foreach (var card in Hand) {
                score += card.Value;
                if (card.Rank == "Ace") aceCount++;
            }

            while (score > 21 && aceCount > 0) {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        public List<string> GetHandDisplay(bool hideFirstCard = false)
        {
            List<string> display = new List<string>();
            for (int i = 0; i < Hand.Count; i++)
            {
                string[] cardLines = ConsoleHelper.GetCardAscii(Hand[i], i == 0 && hideFirstCard).Split('\n');
                for (int j = 0; j < cardLines.Length; j++)
                {
                    if (display.Count <= j)
                        display.Add(cardLines[j]);
                    else
                        display[j] += " " + cardLines[j];
                }
            }
            return display;
        }

        public void ShowHand(bool hideFirstCard = false)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                if (i == 0 && hideFirstCard)
                {
                    Hand[i].Display(true);
                }
                else
                {
                    Hand[i].Display();
                }
                Console.WriteLine();
            }
            // Only show the score if we're not hiding any cards
            if (!hideFirstCard)
            {
                ConsoleHelper.WriteLineColor($"Score: {Score}", ConsoleColor.Yellow);
            }
        }
    }
}
