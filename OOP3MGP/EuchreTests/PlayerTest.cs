using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Euchre.Models;
using Euchre.Shared;

namespace EuchreTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestAddCard()
        {
            Player TestPlayer = new Player("Test Name", null, 1);
            Card card = new Card(Constants.Suit.Diamonds, Constants.Rank.Jack);
            TestPlayer.AddCard(card);
            Assert.Equals(TestPlayer.Hand.Count, 1);
        }
    }
}
