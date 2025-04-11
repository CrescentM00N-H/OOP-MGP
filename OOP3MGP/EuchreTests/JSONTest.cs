using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Euchre.Models; // Assuming your Player and Card classes are here
using Euchre.Shared;
using Euchre.Services; // Where your JsonService class lives

namespace EuchreTests
{
    [TestClass]
    public class JsonServiceTests
    {
        private string singleObjectPath = "test_player.json";
        private string listPath = "test_players.json";

        [TestInitialize]
        public void Setup()
        {
            // Clean up test files if they exist
            if (File.Exists(singleObjectPath)) File.Delete(singleObjectPath);
            if (File.Exists(listPath)) File.Delete(listPath);
        }

        [TestMethod]
        public void SaveToJsonFile_SerializesSingleObject()
        {
            var player = new Player("TestPlayer", new List<Card>(), 5);
            JsonService.SaveToJsonFile(player, singleObjectPath);

            Assert.IsTrue(File.Exists(singleObjectPath));
            string content = File.ReadAllText(singleObjectPath);
            Assert.IsTrue(content.Contains("TestPlayer"));
        }

        [TestMethod]
        public void LoadFromJsonFile_DeserializesSingleObject()
        {
            var player = new Player("TestPlayer", new List<Card>(), 5);
            JsonService.SaveToJsonFile(player, singleObjectPath);

            var loaded = JsonService.LoadFromJsonFile<Player>(singleObjectPath);
            Assert.IsNotNull(loaded);
            Assert.AreEqual("TestPlayer", loaded.Name);
            Assert.AreEqual(5, loaded.Points);
        }

        [TestMethod]
        public void SaveToJsonFile_SerializesListOfObjects()
        {
            var players = new List<Player>
            {
                new Player("Alice", new List<Card>(), 10),
                new Player("Bob", new List<Card>(), 15)
            };

            JsonService.SaveToJsonFile(players, listPath);

            Assert.IsTrue(File.Exists(listPath));
            string content = File.ReadAllText(listPath);
            Assert.IsTrue(content.Contains("Alice"));
            Assert.IsTrue(content.Contains("Bob"));
        }

        [TestMethod]
        public void LoadFromJsonFile_DeserializesListOfObjects()
        {
            var players = new List<Player>
            {
                new Player("Alice", new List<Card>(), 10),
                new Player("Bob", new List<Card>(), 15)
            };

            JsonService.SaveToJsonFile(players, listPath);

            var loaded = JsonService.LoadFromJsonFile<List<Player>>(listPath);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(2, loaded.Count);
            Assert.AreEqual("Alice", loaded[0].Name);
            Assert.AreEqual("Bob", loaded[1].Name);
        }

        [TestMethod]
        public void LoadFromJsonFile_ReturnsDefaultOnInvalidPath()
        {
            var result = JsonService.LoadFromJsonFile<Player>("nonexistent.json");
            Assert.IsNull(result); // default for reference type
        }
    }
}
