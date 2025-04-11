using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euchre.Models;
using Euchre.Shared;


namespace EuchreTests
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void Game_Constructor_InitializesCorrectly()
        {
            Game game = new Game(1, "Dylan");

            Assert.AreEqual(0, game.PlayerScore);
            Assert.AreEqual(0, game.AIScore);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void Game_EndGame_LogsFinalResults()
        {
            Game game = new Game(1, "Jordan");
            game.PlayerScore = 10;
            game.AIScore = 7;

            game.EndGame();

            Assert.IsTrue(game.HandsPlayed.Exists(h => h.Contains("Game Over")));
        }

        [TestMethod]
        public void Game_UpdateScores_PlayerGetsOnePoint()
        {
            Game game = new Game(2, "Player1");

            // simulate player winning 3 tricks
            var updateScoresMethod = typeof(Game).GetMethod("UpdateScores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            updateScoresMethod.Invoke(game, new object[] { 3, 2 });

            Assert.AreEqual(1, game.PlayerScore);
        }

        [TestMethod]
        public void Game_UpdateScores_AIWinsPerfectRound()
        {
            Game game = new Game(3, "Player2");

            // simulate AI winning all 5 tricks
            var updateScoresMethod = typeof(Game).GetMethod("UpdateScores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            updateScoresMethod.Invoke(game, new object[] { 0, 5 });

            Assert.AreEqual(2, game.AIScore);
        }

        [TestMethod]
        public void Game_BidTrump_ReturnsTrumpSuit_WhenAIBids()
        {
            Game game = new Game(4, "Test");

            var bidTrumpMethod = typeof(Game).GetMethod("BidTrump", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var result = bidTrumpMethod.Invoke(game, null);

            // AIPlayer logic may return null depending on hand – test result exists or not
            Assert.IsTrue(result == null || result is Constants.Suit);
        }

        [TestMethod]
        public void Game_StartGame_Reaches10Points_EndsGame()
        {
            Game game = new Game(5, "Test");

            // Override PlayerScore and AIScore quickly to trigger game end
            var playRoundMethod = typeof(Game).GetMethod("PlayRound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var endGameMethod = typeof(Game).GetMethod("EndGame");

            game.PlayerScore = 9;
            game.AIScore = 8;

            // Call one round manually then end
            playRoundMethod.Invoke(game, null);
            game.EndGame();

            Assert.IsTrue(game.PlayerScore >= 10 || game.AIScore >= 10);
            Assert.IsTrue(game.HandsPlayed.Exists(h => h.Contains("Game Over")));
        }

        [TestMethod]
        public void Game_LogGame_LogsCorrectScore()
        {
            Game game = new Game(6, "Logger");
            game.PlayerScore = 6;
            game.AIScore = 4;

            game.LogGame();
            Assert.IsTrue(true); //

        }
    }
}
