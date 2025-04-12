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
            { "Ace,Clubs",         "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ace,Spades.png" },
            { "Ace,Diamonds",      "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ace,Diamonds.png" },
            { "Ace,Hearts",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ace,Hearts.png" },
            { "Ace,Spades",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ace,Spades.png" },

            // Nines
            { "Nine,Clubs",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Nine,Clubs.png" },
            { "Nine,Diamonds",     "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Nine,Diamonds.png" },
            { "Nine,Hearts",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Nine,Hearts.png" },
            { "Nine,Spades",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Nine,Spades.png" },

            // Tens
            { "Ten,Clubs",         "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ten,Clubs.png" },
            { "Ten,Diamonds",      "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ten,Diamonds.png" },
            { "Ten,Hearts",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ten,Hearts.png" },
            { "Ten,Spades",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Ten,Spades.png" },

            // Jacks
            { "Jack,Clubs",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Jack,Clubs.png" },
            { "Jack,Diamonds",     "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Jack,Diamonds.png" },
            { "Jack,Hearts",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Jack,Hearts.png" },
            { "Jack,Spades",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Jack,Spades.png" },

            // Queens
            { "Queen,Clubs",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Queen,Clubs.png" },
            { "Queen,Diamonds",    "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Queen,Diamonds.png" },
            { "Queen,Hearts",      "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Queen,Hearts.png" },
            { "Queen,Spades",      "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\Queen,Spades.png" },

            // Kings
            { "King,Clubs",        "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\King,Clubs.png" },
            { "King,Diamonds",     "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\King,Diamonds.png" },
            { "King,Hearts",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\King,Hearts.png" },
            { "King,Spades",       "C:\\downloads-\\EuchreRepo\\OOP3MGP\\Euchre\\Cards\\PixelTheme\\King,Spades.png" },

            // Card backs
            { "BlueCardBack",     "Cards/PixelTheme/BlueCardBack.png" },
            { "BlueCardBackSpotted", "Cards/PixelTheme/BlueCardBackSpotted.png" }
        };

    }
}
