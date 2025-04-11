using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euchre.Shared;

namespace Euchre.Models
{
    /// <summary>
    /// Represents a deck of cards used in the Euchre game
    /// </summary>
    public class Deck
    {
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Initializes a new deck with 24 Euchre cards 9-Ace for each suit
        /// </summary>
        public Deck()
        {
            Cards = new List<Card>();
            InitializeDeck();
        }

        /// <summary>
        /// Populates the deck with 24 cards 9-Ace for each suit
        /// </summary>
        private void InitializeDeck()
        {
            Cards.Clear();
            foreach (Constants.Suit suit in Enum.GetValues(typeof(Constants.Suit)))
            {
                foreach (Constants.Rank rank in Enum.GetValues(typeof(Constants.Rank)))
                {
                    Cards.Add(new Card(suit, rank));
                }
            }
        }

        /// <summary>
        /// Shuffles the deck using the Fisher Yates algorithm
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card temp = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = temp;
            }
        }

        /// <summary>
        /// Deals a specified number of cards from the top of the deck
        /// </summary>
        /// <param name="count">Number of cards to deal</param>
        /// <returns>A list of dealt cards</returns>
        public List<Card> Deal(int count)
        {
            if (count > Cards.Count) throw new InvalidOperationException("Not enough cards to deal.");
            List<Card> hand = Cards.GetRange(0, count);
            Cards.RemoveRange(0, count);
            return hand;
        }

        /// <summary>
        /// Draws a single card from the top of the deck
        /// </summary>
        /// <returns>The drawn card, or null if the deck is empty</returns>
        public Card Draw()
        {
            if (Cards.Count == 0) return null;
            Card drawn = Cards[0];
            Cards.RemoveAt(0);
            return drawn;
        }
    }
}
