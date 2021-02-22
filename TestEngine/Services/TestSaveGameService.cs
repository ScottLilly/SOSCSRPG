using System.Linq;
using Engine.Services;
using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.Services
{
    [TestClass]
    public class TestSaveGameService
    {
        [TestMethod]
        public void Test_Restore_0_1_000()
        {
            //GameSession gameSession = 
            //    SaveGameService
            //        .LoadLastSaveOrCreateNew(@".\TestFiles\SavedGames\Game_0_1_000.soscsrpg");

            //// Game session data
            //Assert.AreEqual("0.1.000", gameSession.GameDetails.Version);
            //Assert.AreEqual(0, gameSession.CurrentLocation.XCoordinate);
            //Assert.AreEqual(1, gameSession.CurrentLocation.YCoordinate);

            //// Player data
            //Assert.AreEqual("Scott", gameSession.CurrentPlayer.Name);
            //Assert.AreEqual(18, gameSession.CurrentPlayer.Dexterity);
            //Assert.AreEqual(8, gameSession.CurrentPlayer.CurrentHitPoints);
            //Assert.AreEqual(10, gameSession.CurrentPlayer.MaximumHitPoints);
            //Assert.AreEqual(20, gameSession.CurrentPlayer.ExperiencePoints);
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Level);
            //Assert.AreEqual(1000000, gameSession.CurrentPlayer.Gold);

            //// Player quest data
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Quests.Count);
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Quests[0].PlayerQuest.ID);
            //Assert.IsFalse(gameSession.CurrentPlayer.Quests[0].IsCompleted);

            //// Player recipe data
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Recipes.Count);
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Recipes[0].ID);

            //// Player inventory data
            //Assert.AreEqual(5, gameSession.CurrentPlayer.Inventory.Items.Count);
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(1001)));
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(2001)));
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3001)));
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3002)));
            //Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3003)));
        }
    }
}