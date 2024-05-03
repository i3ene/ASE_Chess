using Logic.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Logic
{
    [TestClass]
    public class BoardPositionTest
    {
        [TestMethod]
        public void TestPositionOperations()
        {
            BoardPosition position1 = new BoardPosition(3, 4);
            BoardPosition position2 = new BoardPosition(1, 2);

            BoardPosition positionAdded = position1 + position2;
            BoardPosition positionSubtraced = position1 - position2;
            BoardPosition positionMultiplied = position1 * position2;
            BoardPosition positionDivided = position1 / position2;

            Assert.AreEqual(positionAdded, new BoardPosition(4, 6));
            Assert.AreEqual(positionSubtraced, new BoardPosition(2, 2));
            Assert.AreEqual(positionMultiplied, new BoardPosition(3, 0));
            Assert.AreEqual(positionDivided, new BoardPosition(3, 2));
        }

        [TestMethod]
        public void TestPositionRollover()
        {
            BoardPosition position1 = new BoardPosition(0, 0);
            BoardPosition position2 = new BoardPosition(1, 1);
            BoardPosition position3 = new BoardPosition(7, 7);

            BoardPosition negativeRollover = position1 - position2;
            BoardPosition positiveRollover = position3 + position2;

            Assert.AreEqual(negativeRollover, new BoardPosition(7, 7));
            Assert.AreEqual(positiveRollover, new BoardPosition(0, 0));
        }

        [TestMethod]
        public void TestPositionNotation()
        {
            BoardPosition position1 = new BoardPosition(0, 0);
            BoardPosition position2 = new BoardPosition(1, 1);
            BoardPosition position3 = new BoardPosition(2, 2);
            BoardPosition position4 = new BoardPosition(3, 3);
            BoardPosition position5 = new BoardPosition(4, 4);
            BoardPosition position6 = new BoardPosition(5, 5);
            BoardPosition position7 = new BoardPosition(6, 6);
            BoardPosition position8 = new BoardPosition(7, 7);

            string notation1 = position1.GetNotation();
            string notation2 = position2.GetNotation();
            string notation3 = position3.GetNotation();
            string notation4 = position4.GetNotation();
            string notation5 = position5.GetNotation();
            string notation6 = position6.GetNotation();
            string notation7 = position7.GetNotation();
            string notation8 = position8.GetNotation();

            Assert.AreEqual(notation1, "A1");
            Assert.AreEqual(notation2, "B2");
            Assert.AreEqual(notation3, "C3");
            Assert.AreEqual(notation4, "D4");
            Assert.AreEqual(notation5, "E5");
            Assert.AreEqual(notation6, "F6");
            Assert.AreEqual(notation7, "G7");
            Assert.AreEqual(notation8, "H8");
        }

        [TestMethod]
        public void TestPositionInstantiation()
        {
            BoardPosition defaultPosition = new BoardPosition();
            BoardPosition positiveRolloverPosition = new BoardPosition(8, 9);
            BoardPosition negativeRolloverPosition = new BoardPosition(-1, -2);
            BoardPosition fromNotationPosition = BoardPosition.FromNotation("A2");
            BoardPosition fromInvalidNotation = BoardPosition.FromNotation("I9#");

            Assert.AreEqual(defaultPosition, new BoardPosition(0, 0));
            Assert.AreEqual(positiveRolloverPosition, new BoardPosition(0, 1));
            Assert.AreEqual(negativeRolloverPosition, new BoardPosition(7, 6));
            Assert.AreEqual(fromNotationPosition, new BoardPosition(0, 1));
            Assert.AreEqual(fromInvalidNotation, new BoardPosition(0, 0));
        }
    }
}
