using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euchre.Shared;

namespace Euchre.Models
{
    public class Player
    {
        #region Class Vars
        public static List<Player> Players = new List<Player>();
        private int nextID = 0;
        #endregion

        #region Properties
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public int ID { get; set; }
        public int Points { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for a player initializes with default values
        /// </summary>
        public Player() : this("Unknown", new List<Card>(), 0)
        {
        }

        /// <summary>
        /// Defines a player in Euchre either AI player or non-AI
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="hand">Player's hand of cards</param>
        /// <param name="points">Player's score</param>
        public Player(string name, List<Card> hand, int points)
        {
            this.Name = name;
            this.Hand = hand;
            this.Points = points;
            this.ID = nextID;
            nextID++;
            Player.Players.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a card to the player's hand
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card)
        {
            if (card == null)
            {
                return;
            }
            Hand.Add(card);
        }

        /// <summary>
        /// Adds a list of cards to the player's hand
        /// </summary>
        /// <param name="cards">Cards to be added</param>
        public void AddCards(List<Card> cards)
        {
            Hand.AddRange(cards);
        }

        /// <summary>
        /// Removes a card from the player's hand
        /// </summary>
        /// <param name="card">Card to remove</param>
        public void RemoveCard(Card card)
        {
            Hand.Remove(card);
        }

        /// <summary>
        /// Returns true if the player has the specified card in hand
        /// </summary>
        /// <param name="card">Card to check for</param>
        /// <returns>True if the card is in hand false otherwise</returns>
        public bool HasCard(Card card)
        {
            foreach (Card c in Hand)
            {
                if (c == card) return true;
            }
            return false;
        }

        /// <summary>
        /// Plays a card from the player's hand removing it if valid
        /// </summary>
        /// <param name="card">Card to be played</param>
        /// <returns>The played card or null if not valid</returns>
        public Card PlayCard(Card card)
        {
            if (HasCard(card))
            {
                Hand.Remove(card);
                return card;
            }
            return null;
        }

        /// <summary>
        /// Checks if the player can follow the suit led
        /// </summary>
        /// <param name="suitLed">The suit led in the trick</param>
        /// <returns>True if the player has a card of the suit led false otherwise</returns>
        public bool CanFollowSuit(Constants.Suit suitLed)
        {
            foreach (var card in Hand)
            {
                if (card.Suit == suitLed && !card.IsTrump) return true;
            }
            return false;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Returns one player based on ID
        /// </summary>
        /// <param name="id">ID to be used to find the player</param>
        /// <returns>The player with the specified ID or null if not found</returns>
        public static Player FindByID(int id)
        {
            foreach (Player player in Players)
            {
                if (player.ID == id) return player;
            }
            return null;
        }

        /// <summary>
        /// Returns the first player with the specified card
        /// </summary>
        /// <param name="card">Card to be used to find a player</param>
        /// <returns>The player with the card or null if not found</returns>
        public static Player FindByCard(Card card)
        {
            foreach (Player player in Players)
            {
                foreach (Card curCard in player.Hand)
                {
                    if (card == curCard) return player;
                }
            }
            return null;
        }
        #endregion
    }
}