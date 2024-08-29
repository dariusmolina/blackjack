using System;
using System.Collections.Generic;

namespace blackjack {
    public class Deck {
        private List<Card> cards;
        private Random random = new Random();

        public Deck() {
            cards = new List<Card>();
            string[] suits = {"Hearts", "Diamonds", "Clubs", "Spades"};
            string[] ranks = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            int[] values = {2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11};

            foreach (var suit in suits) {
                for (int i = 0; i < ranks.Length; i++) {
                    cards.Add(new Card(suit, ranks[i], values[i]));
                }
            }
        }

        public Card DrawCard() {
            int cardIndex = random.Next(cards.Count);
            Card drawnCard = cards[cardIndex];
            cards.RemoveAt(cardIndex);
            return drawnCard;
        }

        public void Shuffle() {
            for (int i = cards.Count - 1; i > 0; i--) {
                int j = random.Next(i + 1);
                var temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
    }
}