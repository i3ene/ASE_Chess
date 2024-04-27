namespace Logic.Pieces
{
    public class PieceFactory
    {
        public readonly PieceService pieceService;

        public PieceFactory(PieceService pieceService)
        {
            this.pieceService = pieceService;
        }

        public List<Piece> CreatePieces()
        {
            List<Piece> pieces = new List<Piece>();
            foreach (PieceColor color in pieceService.GetPieceColors())
            {
                pieces.AddRange(CreatePieces(color));
            }
            return pieces;
        }

        public List<Piece> CreatePieces(PieceColor color)
        {
            List<Piece> pieces = new List<Piece>();
            foreach (PieceType type in pieceService.GetPieceTypes())
            {
                pieces.AddRange(CreatePieces(color, type));
            }
            return pieces;
        }

        public List<Piece> CreatePieces(PieceColor color, PieceType type)
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = pieceService.GetPieceAmount(type); i > 0; i--)
            {
                pieces.Add(CreatePiece(color, type, i));
            }
            return pieces;
        }

        public Piece CreatePiece(PieceColor color, PieceType type, int index)
        {
            return new Piece(color, type, pieceService.GetPiecePosition(color, type, index));
        }
    }
}
