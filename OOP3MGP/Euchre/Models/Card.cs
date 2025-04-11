using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        /// Checks if the card is trump based on the current trump suit including left right bower logic
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

        //
        //
        // in progress below
        //
        //
        //
        //


        /// <summary>
        /// Shows the player's hand of cards in the UI
        /// </summary>
        /// <param name="cards"></param>
        public void ShowPlayerHand(List<Card> cards)
        {

            clearPlayerHand();

            for (int i = 0; i < cards.Count && i < 5; i++)
            {
                var card = cards[i];
                string key = $"{card.Rank}_{card.Suit}";

                if (constraints.cardPaths.TryGetValue(key, out string imagePath))
                {
                    //makes a pb
                    PictureBox pb = new PictureBox
                    {
                        BackgroundImage = Image.FromFile(imagePath),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Width = 100,
                        Height = 150,
                        Margin = new Padding(5),
                        //gives it its own rank,suit through a tag which it keeps its whole life cause its a property of the pb same as the size or colour
                        Tag = key
                    };

                    Panel panel = GetPanelByIndex(i);
                    if (panel != null)
                    {
                        //adds the pb to the panel
                        panel.Controls.Add(pb);
                    }
                }
                else
                { 
                    Console.WriteLine($"image for card {key} not found"); 
                }               
            }
        }


        /// <summary>
        /// Clears the player's hand of cards in the UI
        /// </summary>
        public void clearPlayerHand()
        {
            for (int i = 0; i < 5; i++)
            {
                Panel panel = GetPanelByIndex(i);
                if (panel != null)
                {
                    // Clear the panel
                    panel.Controls.Clear();
                }
            }
        }

        /// <summary>
        /// Removes a card from the player's hand in the UI
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        public void RemoveCard(string rank, string suit)
        {
            string key = $"{rank}_{suit}";

            //loop in reverse over cards(pbs) in controls
            for (int i = 0; i < 5; i++)
            {
                Panel panel = GetPanelByIndex(i);
                // if the control is a pb and its tag(pb property, like a description) matches it removes the card(pb)
                if (panel != null)
                {
                    for (int j = panel.Controls.Count - 1; j >= 0; j--)
                    {
                        if (panel.Controls[j] is PictureBox pb && pb.Tag != null && pb.Tag.ToString() == key)
                        {
                            panel.Controls.RemoveAt(j);
                            return; // Exit the loop after removing the card
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Returns the panel corresponding to the index for displaying cards
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Panel GetPanelByIndex(int index)
        {
            switch (index)
            {
                case 0: return pnlPlayerCard1;
                case 1: return pnlPlayerCard2;
                case 2: return pnlPlayerCard3;
                case 3: return pnlPlayerCard4;
                case 4: return pnlPlayerCard5;
                default: return null;
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
