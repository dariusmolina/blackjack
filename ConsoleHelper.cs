using System;

namespace blackjack
{
    public static class ConsoleHelper
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }

        public static void WriteLineColor(string text, ConsoleColor color)
        {
            WriteColor(text, color);
            Console.WriteLine();
        }

        public static string GetCardAscii(Card card, bool hidden = false)
        {
            if (hidden)
            {
                return @"
┌─────────┐
│░░░░░░░░░│
│░░░░░░░░░│
│░░░░░░░░░│
│░░░░░░░░░│
│░░░░░░░░░│
└─────────┘";
            }

            string symbol = card.Suit switch
            {
                "Hearts" => "♥",
                "Diamonds" => "♦",
                "Clubs" => "♣",
                "Spades" => "♠",
                _ => "?"
            };

            string rank = card.Rank.Length > 2 ? card.Rank[0].ToString() : card.Rank;

            return $@"
┌─────────┐
│{rank,-2}       │
│         │
│    {symbol}    │
│         │
│       {rank,2}│
└─────────┘";
        }
    }
}