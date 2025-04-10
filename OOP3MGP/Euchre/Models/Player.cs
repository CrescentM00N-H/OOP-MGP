using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Models
{
    public class Player
    {
        #region Class Vars
        public static List<Player> Players = new List<Player>();
        private int nextID = 0;
        #endregion

        #region Properties
        public String Name { get; set; }
        public List<Card> Hand { get; set; }
        public int ID { get; set; }
        public int Points { get; set; }
        public bool IsAI { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Defines a player in Euchre, either AI player or non AI.
        /// </summary>
        /// <param name="name">Players Name</param>
        /// <param name="hand">players hand of cards</param>
        /// <param name="points"> players score</param>
        /// <param name="isAI">Determines if player is AI or not.</param>
        public Player(String name, List<Card> hand, int points, bool isAI )
        {
            this.Name = name;
            this.Hand = hand;
            this.Points = points;
            this.ID = nextID;
            this.IsAI = isAI;
            nextID++;
            Player.Players.Add( this );
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a card to the players hand
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card)
        {
            Hand.Add( card );
        }
        /// <summary>
        /// Removes a card from players hand.
        /// </summary>
        /// <param name="card">Card to remove.</param>
        public void RemoveCard(Card card) { Hand.Remove( card ); }
        /// <summary>
        /// Returns true if player has card in hand
        /// </summary>
        /// <param name="card">card to check player for.</param>
        /// <returns>True if card, false if not</returns>
        public bool HasCard(Card card)
        {
            foreach(Card c in Hand)
            {
                if(c == card) return true;
            }
            return false;
        }
        /// <summary>
        /// Returns a card if playable, null if not. Removes card from players hand.
        /// </summary>
        /// <param name="card">Card to be played.</param>
        /// <returns></returns>
        public Card PlayCard(Card card)
        {
            // && Game.ValidCard
            if (HasCard(card))
            {
                Hand.Remove(card);
                return card;
            }
            return null;
        }
        #endregion

        #region Static Methods

        /// <summary>
        /// Returns one player based on ID.
        /// </summary>
        /// <param name="id">ID to be used to find the player</param>
        /// <returns></returns>
        public static Player FindByID(int id)
        {
            foreach (Player player in Players)
            {
                if (player.ID == id) return player;
            }
            return null;
        }
        /// <summary>
        /// Returns the first player with the card passed.
        /// </summary>
        /// <param name="card">Card to be used to find a player with.</param>
        /// <returns></returns>
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
