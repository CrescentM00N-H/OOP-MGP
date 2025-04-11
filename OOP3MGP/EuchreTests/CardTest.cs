using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euchre.Models;
using Euchre.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EuchreTests
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void Card_Constructor_SetsSuitAndRank()
        {
            var card = new Card(Constants.Suit.Hearts, Constants.Rank.Ace);
            Assert.AreEqual(Constants.Suit.Hearts, card.Suit);
            Assert.AreEqual(Constants.Rank.Ace, card.Rank);
            Assert.IsFalse(card.IsTrump);
        }

        [TestMethod]
        public void CheckTrump_SetsIsTrump_ForTrumpSuit()
        {
            var card = new Card(Constants.Suit.Spades, Constants.Rank.King);
            card.CheckTrump(Constants.Suit.Spades);
            Assert.IsTrue(card.IsTrump);
        }

        [TestMethod]
        public void CheckTrump_SetsIsTrump_ForLeftBower()
        {
            var card = new Card(Constants.Suit.Clubs, Constants.Rank.Jack); // Left bower if trump is Spades
            card.CheckTrump(Constants.Suit.Spades);
            Assert.IsTrue(card.IsTrump);
        }

        [TestMethod]
        public void CheckTrump_DoesNotSetIsTrump_ForNonTrumpSuit()
        {
            var card = new Card(Constants.Suit.Hearts, Constants.Rank.Ten);
            card.CheckTrump(Constants.Suit.Spades);
            Assert.IsFalse(card.IsTrump);
        }

        [TestMethod]
        public void Compare_RightBowerWinsOverLeftBower()
        {
            var rightBower = new Card(Constants.Suit.Spades, Constants.Rank.Jack);
            var leftBower = new Card(Constants.Suit.Clubs, Constants.Rank.Jack); // Left bower when trump is Spades

            int result = rightBower.Compare(leftBower, Constants.Suit.Spades, Constants.Suit.Spades);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_LeftBowerBeatsOtherTrump()
        {
            var leftBower = new Card(Constants.Suit.Clubs, Constants.Rank.Jack); // Left bower
            var trumpTen = new Card(Constants.Suit.Spades, Constants.Rank.Ten);  // Regular trump

            int result = leftBower.Compare(trumpTen, Constants.Suit.Hearts, Constants.Suit.Spades);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_SuitLedBeatsOffSuit()
        {
            var card1 = new Card(Constants.Suit.Hearts, Constants.Rank.Ace);
            var card2 = new Card(Constants.Suit.Spades, Constants.Rank.Ace);

            int result = card1.Compare(card2, Constants.Suit.Hearts, Constants.Suit.Diamonds);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Compare_SameSuitHigherRankWins()
        {
            var ten = new Card(Constants.Suit.Clubs, Constants.Rank.Ten);
            var king = new Card(Constants.Suit.Clubs, Constants.Rank.King);

            int result = king.Compare(ten, Constants.Suit.Clubs, Constants.Suit.Spades);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetRankValue_ReturnsCorrectValue()
        {
            var ace = new Card(Constants.Suit.Hearts, Constants.Rank.Ace);
            Assert.AreEqual(6, ace.GetRankValue());
        }

        [TestMethod]
        public void GetRankValue_RightBower()
        {
            var jack = new Card(Constants.Suit.Spades, Constants.Rank.Jack);
            jack.CheckTrump(Constants.Suit.Spades);
            Assert.AreEqual(8, jack.GetRankValue());
        }

        [TestMethod]
        public void GetRankValue_LeftBower()
        {
            var jack = new Card(Constants.Suit.Clubs, Constants.Rank.Jack); 
            jack.CheckTrump(Constants.Suit.Spades);
            Assert.AreEqual(7, jack.GetRankValue());
        }

        [TestMethod]
        public void GetLeftBowerSuit_ReturnsCorrectOppositeColorSuit()
        {
            Assert.AreEqual(Constants.Suit.Diamonds, Card.GetLeftBowerSuit(Constants.Suit.Hearts));
            Assert.AreEqual(Constants.Suit.Hearts, Card.GetLeftBowerSuit(Constants.Suit.Diamonds));
            Assert.AreEqual(Constants.Suit.Clubs, Card.GetLeftBowerSuit(Constants.Suit.Spades));
            Assert.AreEqual(Constants.Suit.Spades, Card.GetLeftBowerSuit(Constants.Suit.Clubs));
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat()
        {
            var card = new Card(Constants.Suit.Spades, Constants.Rank.Queen);
            Assert.AreEqual("Queen of Spades", card.ToString());
        }
    }

}
