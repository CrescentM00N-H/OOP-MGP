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
    public class TrickTest
    {
        [TestMethod]
        public void Trick_PlayerLeads_PlayerWinsTrick()
        {
            // Setup
            var player = new Player("Human", new List<Card>
        {
            new Card(Constants.Suit.Hearts, Constants.Rank.Ace)
        }, 0);

            var ai = new AIPlayer();
            ai.AddCards(new List<Card>
        {
            new Card(Constants.Suit.Clubs, Constants.Rank.King)
        });

            var trick = new Trick();

            var winner = trick.PlayTrick(player, ai, Constants.Suit.Spades, playerLeads: true);

            Assert.AreEqual(player, winner);
        }

        [TestMethod]
        public void Trick_AILeads_AIWinsTrickWithTrump()
        {
            var player = new Player("Human", new List<Card>
        {
            new Card(Constants.Suit.Hearts, Constants.Rank.Ace)
        }, 0);

            var ai = new AIPlayer();
            ai.AddCards(new List<Card>
        {
            new Card(Constants.Suit.Spades, Constants.Rank.Nine) // trump suit
        });

            var trick = new Trick();

            var winner = trick.PlayTrick(player, ai, Constants.Suit.Spades, playerLeads: false);

            Assert.AreEqual(ai, winner);
        }

        [TestMethod]
        public void Trick_CardsPlayed_AreLoggedCorrectly()
        {
            var player = new Player("Dylan", new List<Card>
        {
            new Card(Constants.Suit.Diamonds, Constants.Rank.Queen)
        }, 0);

            var ai = new AIPlayer();
            ai.AddCards(new List<Card>
        {
            new Card(Constants.Suit.Hearts, Constants.Rank.King)
        });

            var trick = new Trick();

            trick.PlayTrick(player, ai, Constants.Suit.Spades, playerLeads: true);

            string result = trick.ToString();

            Assert.IsTrue(result.Contains("Dylan played Queen of Diamonds"));
            Assert.IsTrue(result.Contains("played"));
        }

        [TestMethod]
        public void Trick_PlayerWinsWithTrumpOverLeadSuit()
        {
            var player = new Player("Human", new List<Card>
        {
            new Card(Constants.Suit.Spades, Constants.Rank.Ten) // trump suit
        }, 0);

            var ai = new AIPlayer();
            ai.AddCards(new List<Card>
        {
            new Card(Constants.Suit.Hearts, Constants.Rank.Ace)
        });

            var trick = new Trick();

            var winner = trick.PlayTrick(player, ai, Constants.Suit.Spades, playerLeads: true);

            Assert.AreEqual(player, winner);
        }

        [TestMethod]
        public void Trick_AILeads_PlayerBeatsWithHigherTrump()
        {
            var player = new Player("Human", new List<Card>
        {
            new Card(Constants.Suit.Spades, Constants.Rank.Jack) // Right bower trump
        }, 0);

            var ai = new AIPlayer();
            ai.AddCards(new List<Card>
        {
            new Card(Constants.Suit.Spades, Constants.Rank.Nine)
        });

            var trick = new Trick();

            var winner = trick.PlayTrick(player, ai, Constants.Suit.Spades, playerLeads: false);

            Assert.AreEqual(player, winner);
        }
    }

}
