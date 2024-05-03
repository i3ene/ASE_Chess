using Logic.Boards;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mocks
{
    public class BoardMockup : IPieceRepository
    {
        private readonly List<Piece> pieces;

        public BoardMockup()
        {
            pieces = new List<Piece>();
        }

        public bool AddPiece(Piece piece)
        {
            pieces.Add(piece);
            return true;
        }

        public bool AddPieces(Piece[] pieces)
        {
            return true;
        }

        public IEnumerable<Piece> GetAllPieces()
        {
            return pieces.ToArray();
        }

        public Piece? GetPiece(BoardPosition position)
        {
            return pieces.FirstOrDefault(p => p.position.Equals(position));
        }

        public void RemoveAllPieces()
        {
            return;
        }

        public bool RemovePiece(Piece piece)
        {
            return true;
        }

        public bool RemovePiece(BoardPosition position)
        {
            return true;
        }
    }
}
