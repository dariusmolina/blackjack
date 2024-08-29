namespace blackjack {
    public class Card {
        public string Suit {get; set;}
        public string Rank {get; set;}
        public int Value {get; set;}
        
        public Card(string suit, string rank, int value) {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public ConsoleColor Color => Suit == "Hearts" || Suit == "Diamonds" ? ConsoleColor.Red : ConsoleColor.White;

        public void Display(bool hidden = false)
        {
            string cardAscii = ConsoleHelper.GetCardAscii(this, hidden);
            ConsoleHelper.WriteColor(cardAscii, hidden ? ConsoleColor.DarkGray : Color);
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit} (Value: {Value})";
        }
    }
}