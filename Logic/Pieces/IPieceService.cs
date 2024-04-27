using Logic.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Pieces
{
    public interface IPieceService
    {
        public IEnumerable<PieceColor> GetPieceColors();

        public IEnumerable<PieceType> GetPieceTypes();

        public int GetPieceAmount(PieceType type);

        public BoardPosition GetPiecePosition(PieceColor color, PieceType type, int index);
    }
}
