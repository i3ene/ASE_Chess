using Logic.Boards;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Mocks;

namespace Test.Logic
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestPiecePositionChange()
        {
            BoardMock board = new BoardMock();
            BoardService service = new BoardService(board);
            Piece piece = new Piece(PieceColor.White, PieceType.King, new BoardPosition());
            BoardPosition target = new BoardPosition(1, 1);

            service.Move(piece, target);

            Assert.AreEqual(piece.position, target);
        }

        [TestMethod]
        public void TestPawnStartMove()
        {
            Board board = new Board();
            BoardService boardService = new BoardService(board);
            PieceService pieceService = new PieceService();
            PieceFactory pieceFactory = new PieceFactory(pieceService);
            List<Piece> pieces = pieceFactory.CreatePieces(PieceColor.White, PieceType.Pawn);
            board.AddPieces(pieces.ToArray());

            Piece pawn = pieces[0];
            BoardPosition targetPosition = pawn.position + new BoardPosition(0, 2);
            bool successfull = boardService.Move(pawn, targetPosition);

            Assert.IsTrue(successfull);
        }
    }
}
