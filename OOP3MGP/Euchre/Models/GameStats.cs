using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Models
{
    /// <summary>
    /// Logs game and hand statistics for record keeping
    /// </summary>
    public class GameStats
    {
        private int _accountId;
        private List<string> _gameLogs;

        /// <summary>
        /// Initializes a new GameStats instance for the specified account
        /// </summary>
        /// <param name="accountId">The account ID associated with the stats</param>
        public GameStats(int accountId)
        {
            _accountId = accountId;
            _gameLogs = new List<string>();
        }

        /// <summary>
        /// Logs details of a single hand
        /// </summary>
        /// <param name="playerScore">The human player’s score.</param>
        /// <param name="aiScore">The AI’s score.</param>
        /// <param name="handsPlayed">List of hand details.</param>
        public void LogHand(int playerScore, int aiScore, List<string> handsPlayed)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"Hand for Account {_accountId}:");
            log.AppendLine($"Scores - Player: {playerScore}, AI: {aiScore}");
            foreach (var hand in handsPlayed)
            {
                log.AppendLine(hand);
            }
            _gameLogs.Add(log.ToString());
        }

        /// <summary>
        /// Logs the final game results
        /// </summary>
        /// <param name="playerScore">The human player’s final score</param>
        /// <param name="aiScore">The AI’s final score</param>
        public void LogGame(int playerScore, int aiScore)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"Game for Account {_accountId} Ended:");
            log.AppendLine($"Final Scores - Player: {playerScore}, AI: {aiScore}");
            _gameLogs.Add(log.ToString());
        }

        /// <summary>
        /// Retrieves all logged game statistics
        /// </summary>
        /// <returns>A list of log entries</returns>
        public List<string> GetLogs()
        {
            return _gameLogs;
        }
    }
}
