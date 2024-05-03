using Logic.Boards;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mocks
{
    public class PieceServiceMock : IPieceService
    {
        public int GetPieceAmount(PieceType type) => type switch
        {
            PieceType.Queen or
            PieceType.King => 1,
            PieceType.Pawn => 8,
            PieceType.Knight or
            PieceType.Rook or
            PieceType.Bishop => 2,
            _ => 0
        };

        public IEnumerable<PieceColor> GetPieceColors()
        {
            return [PieceColor.White, PieceColor.Black];
        }

        public BoardPosition GetPiecePosition(PieceColor color, PieceType type, int index)
        {
            return new BoardPosition(0, 0);
        }

        public IEnumerable<PieceType> GetPieceTypes()
        {
            return [PieceType.Rook, PieceType.Pawn, PieceType.King, PieceType.Queen, PieceType.Knight, PieceType.Bishop];
        }
    }
}
