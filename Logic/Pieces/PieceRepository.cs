using Logic.Boards;

namespace Logic.Pieces
{
    public class PieceRepository
    {
        private readonly List<Piece> pieces;

        public PieceRepository()
        {
            pieces = new List<Piece>();
        }

        /// <summary>
        /// Add a piece to the collection
        /// </summary>
        /// <param name="piece"><see cref="Piece"/> to add</param>
        /// <returns><see langword="true"/> if <paramref name="piece"/> was successfully added or <see langword="false"/> if <see cref="BoardPosition"/> of <paramref name="piece"/> was already occupied</returns>
        public bool AddPiece(Piece piece)
        {
            Piece? occupied = GetPiece(piece.position);
            if (occupied != null) return false;

            pieces.Add(piece);
            return true;
        }

        /// <summary>
        /// Add multiple pieces to the collection
        /// </summary>
        /// <param name="pieces"><see cref="Piece"/> elements to add</param>
        /// <returns><see langword="true"/> if all <paramref name="pieces"/> was successfully added or <see langword="false"/> if <see cref="BoardPosition"/> of a single element of <paramref name="pieces"/> was already occupied</returns>
        public bool AddPieces(Piece[] pieces)
        {
            foreach (Piece piece in pieces)
            {
                Piece? occupied = GetPiece(piece.position);
                if (occupied != null) return false;
            }

            this.pieces.AddRange(pieces);
            return true;
        }

        /// <summary>
        /// Remove a <see cref="Piece"/> from the collection
        /// </summary>
        /// <param name="piece"><see cref="Piece"/> to remove</param>
        /// <returns><see langword="true"/> if <paramref name="piece"/> was successfully removed or <see langword="false"/> if it could not be removed or did not exist</returns>
        public bool RemovePiece(Piece piece)
        {
            return pieces.Remove(piece);
        }

        /// <summary>
        /// Remove a <see cref="Piece"/> from the collection
        /// </summary>
        /// <param name="position"><see cref="BoardPosition"/> to remove from</param>
        /// <returns><see langword="true"/> if <see cref="Piece"/> at <paramref name="position"/> was successfully removed or <see langword="false"/> if it could not be removed or did not exist</returns>
        public bool RemovePiece(BoardPosition position)
        {
            Piece? piece = GetPiece(position);
            if (piece is null) return false;
            return pieces.Remove(piece);
        }

        public void RemoveAllPieces()
        {
            pieces.Clear();
        }

        /// <summary>
        /// Retrieves a piece at a specified <see cref="BoardPosition"/>
        /// </summary>
        /// <param name="position"><see cref="BoardPosition"/> to fetch from</param>
        /// <returns><see cref="Piece"/> or <see langword="null"/> if <paramref name="position"/> is empty</returns>
        public Piece? GetPiece(BoardPosition position)
        {
            return pieces.FirstOrDefault(p => p.position == position);
        }

        /// <summary>
        /// Retrieve all available pieces
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of pieces</returns>
        public IEnumerable<Piece> GetAllPieces()
        {
            return pieces.ToList();
        }
    }
}
