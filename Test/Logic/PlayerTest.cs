using Logic.Pieces;
using Logic.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Logic
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlayerRepository()
        {
            PlayerRepository repository = new PlayerRepository();
            Player playerWhite = new Player(PieceColor.White);
            Player playerBlack = new Player(PieceColor.Black);
            Player playerDuplicateWhite = new Player(PieceColor.White);

            repository.AddPlayer(playerWhite);
            repository.AddPlayer(playerBlack);
            bool successfull = repository.AddPlayer(playerDuplicateWhite);

            Assert.AreEqual(repository.GetAllPlayers().Count(), 2);
            Assert.IsFalse(successfull);
        }

        [TestMethod]
        public void TestPlayerFactoryCreation()
        {
            PlayerRepository repository = new PlayerRepository();
            PieceService service = new PieceService();
            PlayerFactory factory = new PlayerFactory(service);

            List<Player> players = factory.CreatePlayers();
            repository.AddPlayers(players.ToArray());

            Assert.AreEqual(repository.GetAllPlayers().Count(), 2);
        }
    }
}
