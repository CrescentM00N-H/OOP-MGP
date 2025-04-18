﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        /// <summary>
        /// shows players hand on the ui using panels to display an image with the cooresponding rank and suit. takes a key from the player object
        /// including the rank,suit of a card then gets the image by path using the rank,suit to put it on the panel
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="pnl1"></param>
        /// <param name="pnl2"></param>
        /// <param name="pnl3"></param>
        /// <param name="pnl4"></param>
        /// <param name="pnl5"></param>
        public static void ShowPlayerHand(List<Card> cards, Panel pnl1, Panel pnl2, Panel pnl3, Panel pnl4, Panel pnl5)
        {
            clearPlayerHand(pnl1, pnl2, pnl3, pnl4, pnl5);

            //loop through the player's cards limiting to 5.
            for (int i = 0; i < cards.Count && i < 5; i++)
            {
                var card = cards[i];
                //create a key in the format rank suit to match the dictionary.
                string key = $"{card.Rank},{card.Suit}";

                //get the path from the dictionary in constants
                Console.WriteLine(key);
                string filePath = Constants.PixelThemePaths[key];

                PictureBox pb = new PictureBox
                {
                    BackColor = Color.Red,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Width = 100,
                    Height = 150,
                    Margin = new Padding(5),
                    Tag = key,
                    // Load the image from the file path.
                    BackgroundImage = Image.FromFile(filePath)
                };

                //get the panel based on index
                Panel panel = GetPanelByIndex(i, pnl1, pnl2, pnl3, pnl4, pnl5);
                if (panel != null)
                {
                    panel.Controls.Add(pb);
                }
            }
        }
    
        
        /// <summary>
        /// renmoves all player cards from ui
        /// </summary>
        /// <param name="pnl1"></param>
        /// <param name="pnl2"></param>
        /// <param name="pnl3"></param>
        /// <param name="pnl4"></param>
        /// <param name="pnl5"></param>
        public static void clearPlayerHand(Panel pnl1, Panel pnl2, Panel pnl3, Panel pnl4, Panel pnl5)
        {
            Panel[] panels = new Panel[] { pnl1, pnl2, pnl3, pnl4, pnl5 };
            foreach (Panel pnl in panels)
            {
                pnl?.Controls.Clear();
            }
        }

        /// <summary>
        /// returns the panel specified by index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pnl1"></param>
        /// <param name="pnl2"></param>
        /// <param name="pnl3"></param>
        /// <param name="pnl4"></param>
        /// <param name="pnl5"></param>
        /// <returns></returns>
        public static Panel GetPanelByIndex(int index, Panel pnl1, Panel pnl2, Panel pnl3, Panel pnl4, Panel pnl5)
        {
            switch (index)
            {
                case 0: return pnl1;
                case 1: return pnl2;
                case 2: return pnl3;
                case 3: return pnl4;
                case 4: return pnl5;
                default: return null;
            }
        }

        /// <summary>
        /// removes a card from the ui specified by the cards rank and suit
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="pnl1"></param>
        /// <param name="pnl2"></param>
        /// <param name="pnl3"></param>
        /// <param name="pnl4"></param>
        /// <param name="pnl5"></param>
        public static void RemoveCard(string rank, string suit, Panel pnl1, Panel pnl2, Panel pnl3, Panel pnl4, Panel pnl5)
        {
            string key = $"{rank}_{suit}";
            Panel[] panels = new Panel[] { pnl1, pnl2, pnl3, pnl4, pnl5 };

            foreach (Panel pnl in panels)
            {
                //Loop backwards to safely remove controls
                for (int i = pnl.Controls.Count - 1; i >= 0; i--)
                {
                    if (pnl.Controls[i] is PictureBox pb && pb.Tag?.ToString() == key)
                    {
                        pnl.Controls.RemoveAt(i);
                        return;
                    }
                }
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
