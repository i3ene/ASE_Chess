using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Boards
{
    public interface IBoardService
    {
        public bool Move(Piece piece, BoardPosition position);

        public bool IsMovePossible(Piece piece, BoardPosition position);

        public bool IsPositionOccupied(BoardPosition position);

        public bool IsPieceCapturing(Piece piece, BoardPosition position);

        public bool IsPathBlocked(Piece piece, BoardPosition position);

        public bool SetsCheck(Piece piece, BoardPosition position);

        public PieceColor? IsCheck();
    }
}
