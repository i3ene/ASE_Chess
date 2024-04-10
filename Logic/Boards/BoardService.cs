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
            Piece? targetPiece = pieceRepository.GetPiece(position);
            if (targetPiece != null && targetPiece.color == piece.color) return false;
            // TODO: Check if piece can move
            return true;
        }

        /// <summary>
        /// Checks if a color is in check.
        /// </summary>
        /// <returns><see cref="PieceColor"/> of player in check or <see langword="null"/> if no check is present.</returns>
        public PieceColor? IsCheck()
        {
            return null;
        }
    }
}
