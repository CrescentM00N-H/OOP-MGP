using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Shared
{
    public static class Constants
    {
        #region Enums
        public enum Suit { Hearts, Diamonds, Spades, Clubs}

        public enum Rank { Nine, Ten, Jack, Queen, King, Ace }
        #endregion

        public static readonly Dictionary<string, string> PixelThemePaths
        = new Dictionary<string, string>
        {
            // Aces
            { "Ace,Clubs",         "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Aczzze,Spades.png" },
            { "Ace,Diamonds",      "Cards/PixelTheme/Ace,Diamonds.png" },
            { "Ace,Hearts",        "Cards/PixelTheme/Ace,Hearts.png" },
            { "Ace,Spades",        "Cards/PixelTheme/Ace,Spades.png" },

            // Nines
            { "Nine,Clubs",        "Cards/PixelTheme/Nine,Clubs.png" },
            { "Nine,Diamonds",     "Cards/PixelTheme/Nine,Diamonds.png" },
            { "Nine,Hearts",       "Cards/PixelTheme/Nine,Hearts.png" },
            { "Nine,Spades",       "Cards/PixelTheme/Nine,Spades.png" },

            // Tens
            { "Ten,Clubs",         "Cards/PixelTheme/Ten,Clubs.png" },
            { "Ten,Diamonds",      "Cards/PixelTheme/Ten,Diamonds.png" },
            { "Ten,Hearts",        "Cards/PixelTheme/Ten,Hearts.png" },
            { "Ten,Spades",        "Cards/PixelTheme/Ten,Spades.png" },

            // Jacks
            { "Jack,Clubs",        "Cards/PixelTheme/Jack,Clubs.png" },
            { "Jack,Diamonds",     "Cards/PixelTheme/Jack,Diamonds.png" },
            { "Jack,Hearts",       "Cards/PixelTheme/Jack,Hearts.png" },
            { "Jack,Spades",       "Cards/PixelTheme/Jack,Spades.png" },

            // Queens
            { "Queen,Clubs",       "Cards/PixelTheme/Queen,Clubs.png" },
            { "Queen,Diamonds",    "Cards/PixelTheme/Queen,Diamonds.png" },
            { "Queen,Hearts",      "Cards/PixelTheme/Queen,Hearts.png" },
            { "Queen,Spades",      "Cards/PixelTheme/Queen,Spades.png" },

            // Kings
            { "King,Clubs",        "Cards/PixelTheme/King,Clubs.png" },
            { "King,Diamonds",     "Cards/PixelTheme/King,Diamonds.png" },
            { "King,Hearts",       "Cards/PixelTheme/King,Hearts.png" },
            { "King,Spades",       "Cards/PixelTheme/King,Spades.png" },

            // Card backs
            { "BlueCardBack",     "Cards/PixelTheme/BlueCardBack.png" },
            { "BlueCardBackSpotted", "Cards/PixelTheme/BlueCardBackSpotted.png" }
        };

    }
}
