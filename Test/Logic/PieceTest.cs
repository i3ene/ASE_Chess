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
    public class PieceTest
    {
        [TestMethod]
        public void TestPieceEquality()
        {
            Piece piece1 = new Piece(PieceColor.White, PieceType.King, new BoardPosition());
            Piece piece2 = new Piece(PieceColor.White, PieceType.King, new BoardPosition());

            bool areEqual = piece1.Equals(piece2);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void TestPieceFactoryCreation()
        {
            PieceServiceMock service = new PieceServiceMock();
            PieceFactory factory = new PieceFactory(service);

            
            List<Piece> whitePawns = factory.CreatePieces(PieceColor.White, PieceType.Pawn);
            List<Piece> whiteBishops = factory.CreatePieces(PieceColor.White, PieceType.Bishop);
            List<Piece> whiteKings = factory.CreatePieces(PieceColor.White, PieceType.King);
            List<Piece> whiteQueens = factory.CreatePieces(PieceColor.White, PieceType.Queen);
            List<Piece> whiteRooks = factory.CreatePieces(PieceColor.White, PieceType.Rook);
            List<Piece> whiteKnights = factory.CreatePieces(PieceColor.White, PieceType.Knight);

            List<Piece> blackPawns = factory.CreatePieces(PieceColor.Black, PieceType.Pawn);
            List<Piece> blackBishops = factory.CreatePieces(PieceColor.Black, PieceType.Bishop);
            List<Piece> blackKings = factory.CreatePieces(PieceColor.Black, PieceType.King);
            List<Piece> blackQueens = factory.CreatePieces(PieceColor.Black, PieceType.Queen);
            List<Piece> blackRooks = factory.CreatePieces(PieceColor.Black, PieceType.Rook);
            List<Piece> blackKnights = factory.CreatePieces(PieceColor.Black, PieceType.Knight);

            List<Piece> whitePieces = factory.CreatePieces(PieceColor.White);
            List<Piece> blackPieces = factory.CreatePieces(PieceColor.Black);

            List<Piece> pieces = factory.CreatePieces();

            
            Assert.AreEqual(whitePawns.Count, 8);
            Assert.AreEqual(whiteBishops.Count, 2);
            Assert.AreEqual(whiteKings.Count, 1);
            Assert.AreEqual(whiteQueens.Count, 1);
            Assert.AreEqual(whiteRooks.Count, 2);
            Assert.AreEqual(whiteKnights.Count, 2);

            Assert.AreEqual(blackPawns.Count, 8);
            Assert.AreEqual(blackBishops.Count, 2);
            Assert.AreEqual(blackKings.Count, 1);
            Assert.AreEqual(blackQueens.Count, 1);
            Assert.AreEqual(blackRooks.Count, 2);
            Assert.AreEqual(blackKnights.Count, 2);

            Assert.AreEqual(whitePieces.Count, 16);
            Assert.AreEqual(blackPieces.Count, 16);

            Assert.AreEqual(pieces.Count, 32);
        }

        [TestMethod]
        public void TestPieceServiceAmounts()
        {
            PieceService service = new PieceService();

            int pawnAmount = service.GetPieceAmount(PieceType.Pawn);
            int bishopAmount = service.GetPieceAmount(PieceType.Bishop);
            int kingAmount = service.GetPieceAmount(PieceType.King);
            int queenAmount = service.GetPieceAmount(PieceType.Queen);
            int rookAmount = service.GetPieceAmount(PieceType.Rook);
            int knightAmount = service.GetPieceAmount(PieceType.Knight);

            Assert.AreEqual(pawnAmount, 8);
            Assert.AreEqual(bishopAmount, 2);
            Assert.AreEqual(kingAmount, 1);
            Assert.AreEqual(queenAmount, 1);
            Assert.AreEqual(rookAmount, 2);
            Assert.AreEqual(knightAmount, 2);
        }
    }
}
