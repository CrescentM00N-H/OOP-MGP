using Euchre.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Euchre.Models
{
    /// <summary>
    /// Represents an AI player in the Euchre game capable of bidding and playing cards strategically
    /// </summary>
    public class AIPlayer : Player
    {
        /// <summary>
        /// Initializes a new AI player with a default name
        /// </summary>
        public AIPlayer() : base("AI Opponent", new List<Card>(), 0)
        {
        }

        /// <summary>
        /// Decides whether to bid on a trump suit based on the upcard
        /// </summary>
        /// <param name="upcard">The face up card from the deck</param>
        /// <returns>The chosen trump suit or null if passing</returns>
        public Constants.Suit? Bid(Card upcard)
        {
            int trumpCount = Hand.Count(card => card.Suit == upcard.Suit ||
                                               (card.Rank == Constants.Rank.Jack &&
                                                Card.GetLeftBowerSuit(upcard.Suit) == card.Suit));
            int highCards = Hand.Count(card => card.Rank >= Constants.Rank.Queen);

            // Bid if strong in the suit 
            if (trumpCount >= 3 || (trumpCount >= 2 && highCards >= 2))
            {
                return upcard.Suit;
            }

            // Pass
            return null; 
        }

        /// <summary>
        /// Chooses a trump suit if forced to pick during the second round of bidding
        /// </summary>
        /// <returns>The chosen trump suit or null if no good option</returns>
        public Constants.Suit? ChooseTrump()
        {
            // Count cards per suit
            var suitCounts = new Dictionary<Constants.Suit, int>();
            foreach (var suit in Enum.GetValues(typeof(Constants.Suit)))
            {
                Constants.Suit s = (Constants.Suit)suit;
                int count = Hand.Count(card => card.Suit == s ||
                                              (card.Rank == Constants.Rank.Jack &&
                                               Card.GetLeftBowerSuit(s) == card.Suit));
                suitCounts[s] = count;
            }

            // Pick suit with most cards
            var bestSuit = suitCounts.OrderByDescending(x => x.Value).FirstOrDefault();
            if (bestSuit.Value >= 3)
            {
                return bestSuit.Key;
            }

            return null; 
        }

        /// <summary>
        /// Plays a card based on the current trick state
        /// </summary>
        /// <param name="suitLed">The suit led in the trick</param>
        /// <param name="trumpSuit">The trump suit for the round</param>
        /// <param name="cardsPlayed">Cards already played in the trick</param>
        /// <returns>The card to play</returns>
        public Card PlayCard(Constants.Suit? suitLed, Constants.Suit trumpSuit, List<Card> cardsPlayed)
        {
            UpdateTrumpStatus(trumpSuit);

            // Declare trumpCards at method scope to avoid redeclaration
            List<Card> trumpCards = Hand.Where(c => c.IsTrump).OrderByDescending(c => c.GetRankValue()).ToList();

            // If leading the trick
            if (!suitLed.HasValue)
            {
                // Play highest trump if available
                if (trumpCards.Any())
                {
                    Card card = trumpCards.First();
                    base.PlayCard(card);
                    return card;
                }
                // Otherwise play highest non-trump card
                var nonTrump = Hand.OrderByDescending(c => c.GetRankValue()).First();
                base.PlayCard(nonTrump);
                return nonTrump;
            }

            // If following try to follow suit
            if (CanFollowSuit(suitLed.Value))
            {
                var validCards = Hand.Where(c => c.Suit == suitLed.Value && !c.IsTrump)
                                     .OrderByDescending(c => c.GetRankValue()).ToList();
                Card card = validCards.First(); 
                base.PlayCard(card);
                return card;
            }

            // If can’t follow play trump if winning
            if (trumpCards.Any())
            {
                Card card = trumpCards.First();
                base.PlayCard(card);
                return card;
            }

            // Otherwise play lowest card
            Card lowest = Hand.OrderBy(c => c.GetRankValue()).First();
            base.PlayCard(lowest);
            return lowest;
        }

        /// <summary>
        /// Updates the trump status of all cards in hand
        /// </summary>
        /// <param name="trumpSuit">The trump suit for the round</param>
        private void UpdateTrumpStatus(Constants.Suit trumpSuit)
        {
            foreach (var card in Hand)
            {
                card.CheckTrump(trumpSuit);
            }
        }
    }
}