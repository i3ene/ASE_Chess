using Logic;
using Logic.Boards;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Logic
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestChangeTurn()
        {
            Game game = new Game();

            PieceColor firstTurn = game.currentColor;
            game.Turn();
            PieceColor nextTurn = game.currentColor;

            Assert.AreEqual(firstTurn, PieceColor.White);
            Assert.AreEqual(nextTurn, PieceColor.Black);
        }

        [TestMethod]
        public void TestFirstMove()
        {
            Game game = new Game();
            BoardPosition sourcePosition = new BoardPosition(0, 1);
            BoardPosition targetPosition = new BoardPosition(0, 2);

            bool successfull = game.Move(sourcePosition, targetPosition);

            Assert.IsTrue(successfull);
        }
    }
}
