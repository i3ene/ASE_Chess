using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Boards
{
    public class BoardService
    {
        public readonly PieceRepository pieceRepository;

        public BoardService(PieceRepository pieceRepository)
        {
            this.pieceRepository = pieceRepository;
        }

        public bool Move(Piece piece, BoardPosition position)
        {
            bool isPossible = IsMovePossible(piece, position);
            if (!isPossible) return false;
            pieceRepository.RemovePiece(piece);
            piece.position.Set(position);
            return true;
        }

        public bool IsMovePossible(Piece piece, BoardPosition position)
        {
            // TODO
            return false;
        }
    }
}
