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
            { "AceClubs",         "Cards/PixelTheme/Ace,Clubs.png" },
            { "AceDiamonds",      "Cards/PixelTheme/Ace,Diamonds.png" },
            { "AceHearts",        "Cards/PixelTheme/Ace,Hearts.png" },
            { "AceSpades",        "Cards/PixelTheme/Ace,Spades.png" },

            // Nines
            { "NineClubs",        "Cards/PixelTheme/Nine,Clubs.png" },
            { "NineDiamonds",     "Cards/PixelTheme/Nine,Diamonds.png" },
            { "NineHearts",       "Cards/PixelTheme/Nine,Hearts.png" },
            { "NineSpades",       "Cards/PixelTheme/Nine,Spades.png" },

            // Tens
            { "TenClubs",         "Cards/PixelTheme/Ten,Clubs.png" },
            { "TenDiamonds",      "Cards/PixelTheme/Ten,Diamonds.png" },
            { "TenHearts",        "Cards/PixelTheme/Ten,Hearts.png" },
            { "TenSpades",        "Cards/PixelTheme/Ten,Spades.png" },

            // Jacks
            { "JackClubs",        "Cards/PixelTheme/Jack,Clubs.png" },
            { "JackDiamonds",     "Cards/PixelTheme/Jack,Diamonds.png" },
            { "JackHearts",       "Cards/PixelTheme/Jack,Hearts.png" },
            { "JackSpades",       "Cards/PixelTheme/Jack,Spades.png" },

            // Queens
            { "QueenClubs",       "Cards/PixelTheme/Queen,Clubs.png" },
            { "QueenDiamonds",    "Cards/PixelTheme/Queen,Diamonds.png" },
            { "QueenHearts",      "Cards/PixelTheme/Queen,Hearts.png" },
            { "QueenSpades",      "Cards/PixelTheme/Queen,Spades.png" },

            // Kings
            { "KingClubs",        "Cards/PixelTheme/King,Clubs.png" },
            { "KingDiamonds",     "Cards/PixelTheme/King,Diamonds.png" },
            { "KingHearts",       "Cards/PixelTheme/King,Hearts.png" },
            { "KingSpades",       "Cards/PixelTheme/King,Spades.png" },

            // Card backs
            { "BlueCardBack",     "Cards/PixelTheme/BlueCardBack.png" },
            { "BlueCardBackSpotted", "Cards/PixelTheme/BlueCardBackSpotted.png" }
        };

    }
}
