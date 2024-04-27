using Logic.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Pieces
{
    public interface IPieceRepository
    {
        public bool AddPiece(Piece piece);

        public bool AddPieces(Piece[] pieces);

        public bool RemovePiece(Piece piece);

        public bool RemovePiece(BoardPosition position);

        public void RemoveAllPieces();

        public Piece? GetPiece(BoardPosition position);

        public IEnumerable<Piece> GetAllPieces();
    }
}
