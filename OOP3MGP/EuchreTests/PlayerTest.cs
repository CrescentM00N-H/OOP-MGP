using Microsoft.VisualStudio.TestTools.UnitTesting;
using Euchre.Models;
using Euchre.Shared;
using System.Collections.Generic;

namespace Euchre.Tests
{
    [TestClass]
    public class PlayerTests
    {
        private Card testCard;
        private List<Card> testHand;

        [TestInitialize]
        public void Setup()
        {
            testCard = new Card(Constants.Suit.Hearts, Constants.Rank.Ace);
            testHand = new List<Card> { testCard };
            Player.Players.Clear(); // Reset static player list before each test
        }

        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var player = new Player("Alice", testHand, 10);

            Assert.AreEqual("Alice", player.Name);
            Assert.AreEqual(1, player.Hand.Count);
            Assert.AreEqual(testCard, player.Hand[0]);
            Assert.AreEqual(10, player.Points);
        }

        [TestMethod]
        public void AddCard_AddsCardToHand()
        {
            var player = new Player();
            player.AddCard(testCard);

            Assert.IsTrue(player.Hand.Contains(testCard));
        }

        [TestMethod]
        public void AddCard_DoesNotThrow_WhenCardIsNull()
        {
            var player = new Player();
            player.AddCard(null);

            Assert.AreEqual(0, player.Hand.Count);
        }

        [TestMethod]
        public void AddCards_AddsMultipleCardsToHand()
        {
            var player = new Player();
            player.AddCards(testHand);

            Assert.AreEqual(1, player.Hand.Count);
            Assert.AreEqual(testCard, player.Hand[0]);
        }

        [TestMethod]
        public void RemoveCard_RemovesCardFromHand()
        {
            var player = new Player("Bob", new List<Card> { testCard }, 0);
            player.RemoveCard(testCard);

            Assert.IsFalse(player.Hand.Contains(testCard));
        }

        [TestMethod]
        public void HasCard_ReturnsTrueIfCardInHand()
        {
            var player = new Player("Bob", new List<Card> { testCard }, 0);
            bool hasCard = player.HasCard(testCard);

            Assert.IsTrue(hasCard);
        }

        [TestMethod]
        public void HasCard_ReturnsFalseIfCardNotInHand()
        {
            var player = new Player();
            bool hasCard = player.HasCard(testCard);

            Assert.IsFalse(hasCard);
        }

        [TestMethod]
        public void PlayCard_RemovesCardAndReturnsIt_WhenValid()
        {
            var player = new Player("Bob", new List<Card> { testCard }, 0);
            var played = player.PlayCard(testCard);

            Assert.AreEqual(testCard, played);
            Assert.IsFalse(player.Hand.Contains(testCard));
        }

        [TestMethod]
        public void PlayCard_ReturnsNull_WhenCardNotInHand()
        {
            var player = new Player();
            var result = player.PlayCard(testCard);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanFollowSuit_ReturnsTrue_WhenMatchingSuitNonTrumpExists()
        {
            testCard.IsTrump = false;
            var player = new Player("Bob", new List<Card> { testCard }, 0);

            bool canFollow = player.CanFollowSuit(Constants.Suit.Hearts);

            Assert.IsTrue(canFollow);
        }

        [TestMethod]
        public void CanFollowSuit_ReturnsFalse_WhenNoMatchingSuit()
        {
            var spadeCard = new Card(Constants.Suit.Spades, Constants.Rank.King);
            var player = new Player("Bob", new List<Card> { spadeCard }, 0);

            bool canFollow = player.CanFollowSuit(Constants.Suit.Hearts);

            Assert.IsFalse(canFollow);
        }

        [TestMethod]
        public void FindByID_ReturnsCorrectPlayer()
        {
            var player = new Player("Alice", testHand, 0);
            var found = Player.FindByID(player.ID);

            Assert.AreEqual(player, found);
        }

        [TestMethod]
        public void FindByID_ReturnsNull_IfNotFound()
        {
            var found = Player.FindByID(999);
            Assert.IsNull(found);
        }

        [TestMethod]
        public void FindByCard_ReturnsCorrectPlayer()
        {
            var player = new Player("Alice", new List<Card> { testCard }, 0);
            var found = Player.FindByCard(testCard);

            Assert.AreEqual(player, found);
        }

        [TestMethod]
        public void FindByCard_ReturnsNull_IfCardNotFound()
        {
            var player = new Player("Alice", new List<Card>(), 0);
            var found = Player.FindByCard(testCard);

            Assert.IsNull(found);
        }
    }
}
