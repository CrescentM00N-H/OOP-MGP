using Euchre.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Models
{
    /// <summary>
    /// Manages the overall flow of a 2 player Euchre game including dealing, bidding, and scoring
    /// </summary>
    public class Game
    {
        public int PlayerScore { get; set; }
        public int AIScore { get; set; }
        public List<string> HandsPlayed { get; set; }
        private Player _humanPlayer;
        private AIPlayer _aiPlayer;
        private Deck _deck;
        private Constants.Suit? _trumpSuit;
        private GameStats _gameStats;

        /// <summary>
        /// Initializes a new game for the specified player
        /// </summary>
        /// <param name="accountId">The player’s account ID</param>
        /// <param name="name">The player’s name</param>
        public Game(int accountId, string name)
        {
            _humanPlayer = new Player(name, new List<Card>(), 0);
            _aiPlayer = new AIPlayer();
            _deck = new Deck();
            PlayerScore = 0;
            AIScore = 0;
            HandsPlayed = new List<string>();
            _gameStats = new GameStats(accountId);
        }

        /// <summary>
        /// Starts a new game playing rounds until a player reaches 10 points
        /// </summary>
        public void StartGame()
        {
            while (PlayerScore < 10 && AIScore < 10)
            {
                PlayRound();
            }
            EndGame();
        }

        /// <summary>
        /// Plays a single hand of Euchre
        /// </summary>
        private void PlayRound()
        {
            // Reset deck and deal cards
            _deck = new Deck();
            _deck.Shuffle();
            _humanPlayer.AddCards(_deck.Deal(5));
            _aiPlayer.AddCards(_deck.Deal(5));

            // Bidding phase
            _trumpSuit = BidTrump();
            if (!_trumpSuit.HasValue) return; 

            // Play 5 tricks
            int playerTricks = 0;
            int aiTricks = 0;
            for (int i = 0; i < 5; i++)
            {
                Trick trick = new Trick();
                var winner = trick.PlayTrick(_humanPlayer, _aiPlayer, _trumpSuit.Value, i % 2 == 0);
                if (winner == _humanPlayer)
                    playerTricks++;
                else
                    aiTricks++;
                HandsPlayed.Add($"Trick {i + 1}: {trick.ToString()}");
            }

            // Update scores
            UpdateScores(playerTricks, aiTricks);
            _gameStats.LogHand(PlayerScore, AIScore, HandsPlayed);
        }

        /// <summary>
        /// Handles the bidding phase to determine the trump suit
        /// </summary>
        /// <returns>The chosen trump suit or null if no trump is selected</returns>
        private Constants.Suit? BidTrump()
        {
            Card upcard = _deck.Draw();
            HandsPlayed.Add($"Upcard: {upcard}");

            // First round: AI bids on upcard
            var aiBid = _aiPlayer.Bid(upcard);
            if (aiBid.HasValue)
            {
                HandsPlayed.Add($"AI bids {aiBid}");
                return aiBid;
            }

            // Second round: AI picks a suit
            aiBid = _aiPlayer.ChooseTrump();
            if (aiBid.HasValue)
            {
                HandsPlayed.Add($"AI chooses {aiBid}");
                return aiBid;
            }

            return null;
        }

        /// <summary>
        /// Updates scores based on tricks won
        /// </summary>
        /// <param name="playerTricks">Number of tricks won by the human player</param>
        /// <param name="aiTricks">Number of tricks won by the AI</param>
        private void UpdateScores(int playerTricks, int aiTricks)
        {
            if (playerTricks >= 3)
            {
                PlayerScore += (playerTricks == 5) ? 2 : 1;
            }
            else if (aiTricks >= 3)
            {
                AIScore += (aiTricks == 5) ? 2 : 1;
            }
            HandsPlayed.Add($"Score: Player {PlayerScore}, AI {AIScore}");
        }

        /// <summary>
        /// Ends the game and logs final results
        /// </summary>
        public void EndGame()
        {
            _gameStats.LogGame(PlayerScore, AIScore);
            HandsPlayed.Add($"Game Over: Player {PlayerScore}, AI {AIScore}");
        }

        /// <summary>
        /// Logs game details to the GameStats instance
        /// </summary>
        public void LogGame()
        {
            _gameStats.LogGame(PlayerScore, AIScore);
        }
    }
}