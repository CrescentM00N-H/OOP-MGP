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
    public class AIPlayerTests
    {
        [TestMethod]
        // Tests that the AI bids on the upcard’s suit when it has a strong hand 3+ cards in the suit
        public void Bid_ShouldBidOnStrongSuit()
        {
            // Arrange
            AIPlayer ai = new AIPlayer();
            ai.AddCards(new List<Card>
            {
                new Card(Constants.Suit.Hearts, Constants.Rank.Ace),
                new Card(Constants.Suit.Hearts, Constants.Rank.King),
                new Card(Constants.Suit.Hearts, Constants.Rank.Queen),
                new Card(Constants.Suit.Spades, Constants.Rank.Nine),
                new Card(Constants.Suit.Diamonds, Constants.Rank.Ten)
            });
            Card upcard = new Card(Constants.Suit.Hearts, Constants.Rank.Jack);

            // Act
            var bid = ai.Bid(upcard);

            // Assert
            Assert.AreEqual(Constants.Suit.Hearts, bid, "AI should bid Hearts with 3+ cards in the suit.");
        }

        [TestMethod]
        // Tests that the AI passes when it has a weak hand in the upcard’s suit
        public void Bid_ShouldPassOnWeakSuit()
        {
            // Arrange
            AIPlayer ai = new AIPlayer();
            ai.AddCards(new List<Card>
            {
                new Card(Constants.Suit.Spades, Constants.Rank.Nine),
                new Card(Constants.Suit.Spades, Constants.Rank.Ten),
                new Card(Constants.Suit.Diamonds, Constants.Rank.Jack),
                new Card(Constants.Suit.Clubs, Constants.Rank.Queen),
                new Card(Constants.Suit.Hearts, Constants.Rank.Nine)
            });
            Card upcard = new Card(Constants.Suit.Hearts, Constants.Rank.Jack);

            // Act
            var bid = ai.Bid(upcard);

            // Assert
            Assert.IsNull(bid, "AI should pass with a weak hand in the upcard's suit.");
        }

        [TestMethod]
        // Tests that the AI plays its highest trump card the right bower when leading a trick
        public void PlayCard_ShouldPlayHighestTrumpWhenLeading()
        {
            // Arrange
            AIPlayer ai = new AIPlayer();
            ai.AddCards(new List<Card>
            {
                new Card(Constants.Suit.Diamonds, Constants.Rank.Jack),
                new Card(Constants.Suit.Hearts, Constants.Rank.Jack),
                new Card(Constants.Suit.Diamonds, Constants.Rank.Ace),
                new Card(Constants.Suit.Spades, Constants.Rank.Nine),
                new Card(Constants.Suit.Clubs, Constants.Rank.Ten)
            });
            Constants.Suit trumpSuit = Constants.Suit.Diamonds;

            // Act
            Card playedCard = ai.PlayCard(null, trumpSuit, new List<Card>());

            // Assert
            Assert.AreEqual(Constants.Rank.Jack, playedCard.Rank, "AI should play the highest trump (right bower).");
            Assert.AreEqual(Constants.Suit.Diamonds, playedCard.Suit, "AI should play the right bower (Jack of Diamonds).");
        }

        [TestMethod]
        // Tests that the AI follows the led suit when it has a card of that suit playing the highest card in that suit
        public void PlayCard_ShouldFollowSuitWhenPossible()
        {
            // Arrange
            AIPlayer ai = new AIPlayer();
            ai.AddCards(new List<Card>
            {
                new Card(Constants.Suit.Spades, Constants.Rank.Ace),
                new Card(Constants.Suit.Spades, Constants.Rank.King),
                new Card(Constants.Suit.Diamonds, Constants.Rank.Jack),
                new Card(Constants.Suit.Clubs, Constants.Rank.Ten),
                new Card(Constants.Suit.Hearts, Constants.Rank.Nine)
            });
            Constants.Suit trumpSuit = Constants.Suit.Diamonds;
            Constants.Suit suitLed = Constants.Suit.Spades;

            // Act
            Card playedCard = ai.PlayCard(suitLed, trumpSuit, new List<Card>());

            // Assert
            Assert.AreEqual(Constants.Suit.Spades, playedCard.Suit, "AI should follow the led suit Spades");
            Assert.AreEqual(Constants.Rank.Ace, playedCard.Rank, "AI should play the highest card in the led suit");
        }
    }
}
