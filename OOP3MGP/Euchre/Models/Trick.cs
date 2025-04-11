using Euchre.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Models
{
    /// <summary>
    /// Represents a single trick in the Euchre game where players play one card each
    /// </summary>
    public class Trick
    {
        private List<Card> _cardsPlayed;
        private List<Player> _playersPlayed;

        /// <summary>
        /// Initializes a new trick
        /// </summary>
        public Trick()
        {
            _cardsPlayed = new List<Card>();
            _playersPlayed = new List<Player>();
        }

        /// <summary>
        /// Plays a trick determining the winner based on cards played
        /// </summary>
        /// <param name="player">The human player</param>
        /// <param name="ai">The AI player</param>
        /// <param name="trumpSuit">The trump suit for the round</param>
        /// <param name="playerLeads">True if the human player leads the trick</param>
        /// <returns>The player who wins the trick</returns>
        public Player PlayTrick(Player player, AIPlayer ai, Constants.Suit trumpSuit, bool playerLeads)
        {
            _cardsPlayed.Clear();
            _playersPlayed.Clear();

            Player firstPlayer = playerLeads ? player : ai;
            Player secondPlayer = playerLeads ? ai : player;

            // First player plays
            Card firstCard;
            if (firstPlayer is AIPlayer aiPlayer)
            {
                firstCard = aiPlayer.PlayCard(null, trumpSuit, _cardsPlayed);
            }
            else
            {
                firstCard = firstPlayer.Hand[0]; 
                firstPlayer.PlayCard(firstCard);
            }
            _cardsPlayed.Add(firstCard);
            _playersPlayed.Add(firstPlayer);

            // Second player plays
            Card secondCard;
            if (secondPlayer is AIPlayer aiPlayer2)
            {
                secondCard = aiPlayer2.PlayCard(firstCard.Suit, trumpSuit, _cardsPlayed);
            }
            else
            {
                secondCard = secondPlayer.Hand[0]; 
                secondPlayer.PlayCard(secondCard);
            }
            _cardsPlayed.Add(secondCard);
            _playersPlayed.Add(secondPlayer);

            // Determine winner
            int comparison = firstCard.Compare(secondCard, firstCard.Suit, trumpSuit);
            return (comparison >= 0) ? firstPlayer : secondPlayer;
        }

        /// <summary>
        /// Returns a string representation of the trick
        /// </summary>
        /// <returns>String describing the cards played.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _cardsPlayed.Count; i++)
            {
                sb.Append($"{_playersPlayed[i].Name} played {_cardsPlayed[i]}; ");
            }
            return sb.ToString().TrimEnd(' ', ';');
        }
    }
}