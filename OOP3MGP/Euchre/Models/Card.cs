﻿using System;
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


        public void ShowPlayerHand(List<Card> cards)
        {
            foreach (var card in cards)
            {
                //make a key with rank and suit to match the naming scheme of the dict containg card png paths
                string key = $"{card.Rank}_{card.Suit}";
                //cardPaths is a dict with cardnames and paths| rankSuit, path
                //   if (constraints.cardPaths.TryGetValue(key, out string imagePath))
                {
                    //makes a pb
                    PictureBox pb = new PictureBox
                    {
                        // BackgroundImage = Image.FromFile(imagePath),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Width = 100,
                        Height = 150,
                        Margin = new Padding(5)
                        //gives it its own rank,suit through a tag which it keeps its whole life cause its a property of the pb same as the size or colour
                        // tag = this.key
                    };

                    // main player cards is a panel the new pb gets put on
                    //mainPlayerCards.Controls.Add(pb);

                } //else{ Console.WriteLine($"image for card {key} not found"); }
                    
                
            }
        }


        public void clearPlayerHand()
        {
            //this is the panel the card images are put into when hand is shown
            // mainPlayerCards.Controls.Clear();
        }

        public void RemoveCard(string rank, string suit)
        {
            string key = $"{rank}_{suit}";

            //loop in reverse over cards(pbs) in controls
         //   for (int i = mainPlayerCards.Controls.Count - 1; i >= 0; i--)
            {
                // if the control is a pb and its tag(pb property, like a description) matches it removes the card(pb)
               // if (mainPlayerCards.Controls[i] is PictureBox pb &&
                 //   pb.Tag != null &&
                 //   pb.Tag.ToString() == key)
                {
                    //remove the card.
                    //mainPlayerCards.Controls.RemoveAt(i);
                  //  break;
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
