using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euchre.Shared;

namespace Euchre.Models
{
    /// <summary>
    /// Represents a single card in the Euchre game with suit, rank, and trump status
    /// </summary>
    public class Card
    {
        public Constants.Suit Suit { get; set; }
        public Constants.Rank Rank { get; set; }
        public bool IsTrump { get; set; }

        /// <summary>
        /// Initializes a new card with the specified suit and rank
        /// </summary>
        /// <param name="suit">The suit of the card Hearts</param>
        /// <param name="rank">The rank of the card Ace</param>
        public Card(Constants.Suit suit, Constants.Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsTrump = false;
        }

        /// <summary>
        /// Checks if the card is trump based on the current trump suit, including left/right bower logic
        /// </summary>
        /// <param name="currentTrump">The trump suit for the round</param>
        public void CheckTrump(Constants.Suit currentTrump)
        {
            IsTrump = (Suit == currentTrump) ||
                      (Rank == Constants.Rank.Jack && GetLeftBowerSuit(currentTrump) == Suit);
        }

        /// <summary>
        /// Compares this card to another card, considering trump and suit led
        /// </summary>
        /// <param name="other">The other card to compare against</param>
        /// <param name="suitLed">The suit led in the trick</param>
        /// <param name="trumpSuit">The trump suit for the round</param>
        /// <returns>Positive if this card wins, negative if other wins, zero if invalid</returns>
        public int Compare(Card other, Constants.Suit suitLed, Constants.Suit trumpSuit)
        {
            CheckTrump(trumpSuit);
            other.CheckTrump(trumpSuit);

            // Trump beats non trump
            if (IsTrump && !other.IsTrump) return 1;
            if (!IsTrump && other.IsTrump) return -1;

            if (IsTrump && other.IsTrump)
            {
                // Right bower beats all
                if (Rank == Constants.Rank.Jack && Suit == trumpSuit) return 1;
                if (other.Rank == Constants.Rank.Jack && other.Suit == trumpSuit) return -1;
                // Left bower beats others except right bower
                if (Rank == Constants.Rank.Jack && GetLeftBowerSuit(trumpSuit) == Suit) return 1;
                if (other.Rank == Constants.Rank.Jack && GetLeftBowerSuit(trumpSuit) == other.Suit) return -1;
                // Compare trump ranks
                return GetRankValue().CompareTo(other.GetRankValue());
            }

            // If neither is trump, must follow suit led
            if (Suit == suitLed && other.Suit != suitLed) return 1;
            if (Suit != suitLed && other.Suit == suitLed) return -1;
            if (Suit == suitLed && other.Suit == suitLed)
            {
                return GetRankValue().CompareTo(other.GetRankValue());
            }

            return 0; 
        }

        /// <summary>
        /// Gets the rank value of the card, adjusted for trump if applicable
        /// </summary>
        /// <returns>An integer representing the card’s value</returns>
        public int GetRankValue()
        {
            if (IsTrump)
            {
                // Left bower
                if (Rank == Constants.Rank.Jack && Suit == GetLeftBowerSuit(Suit)) return 7;
                // Right bower
                if (Rank == Constants.Rank.Jack) return 8; 
            }
            switch (Rank)
            {
                case Constants.Rank.Nine: return 1;
                case Constants.Rank.Ten: return 2;
                case Constants.Rank.Jack: return 3;
                case Constants.Rank.Queen: return 4;
                case Constants.Rank.King: return 5;
                case Constants.Rank.Ace: return 6;
                default: return 0;
            }
        }

        /// <summary>
        /// Determines the suit of the left bower based on the trump suit
        /// </summary>
        /// <param name="trump">The trump suit</param>
        /// <returns>The suit of the left bower</returns>
        public static Constants.Suit GetLeftBowerSuit(Constants.Suit trump)
        {
            switch (trump)
            {
                case Constants.Suit.Hearts: return Constants.Suit.Diamonds;
                case Constants.Suit.Diamonds: return Constants.Suit.Hearts;
                case Constants.Suit.Spades: return Constants.Suit.Clubs;
                case Constants.Suit.Clubs: return Constants.Suit.Spades;
                default: return trump;
            }
        }

        /// <summary>
        /// Returns a string representation of the card
        /// </summary>
        /// <returns>String in the format Rank of Suit</returns>
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
