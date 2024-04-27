using Logic.Boards;

namespace Logic.Pieces
{
    public class PieceService
    {
        public IEnumerable<PieceColor> GetPieceColors()
        {
            return Enum.GetValues(typeof(PieceColor)).Cast<PieceColor>();
        }

        public IEnumerable<PieceType> GetPieceTypes()
        {
            return Enum.GetValues(typeof(PieceType)).Cast<PieceType>();
        }

        public int GetPieceAmount(PieceType type) => type switch
        {
            PieceType.Queen => 1,
            PieceType.King => 1,
            PieceType.Knight => 2,
            PieceType.Bishop => 2,
            PieceType.Rook => 2,
            PieceType.Pawn => 8,
            _ => 0
        };

        public BoardPosition GetPiecePosition(PieceColor color, PieceType type, int index)
        {
            BoardPosition position = new BoardPosition();
            index = Math.Abs(index % GetPieceAmount(type));

            switch (type)
            {
                case PieceType.Queen:
                    position.x = 3;
                    break;
                case PieceType.King:
                    position.x = 4;
                    break;
                case PieceType.Bishop:
                    position.x = index == 0 ? 2 : (BoardPosition.MAX - 3);
                    break;
                case PieceType.Knight:
                    position.x = index == 0 ? 1 : (BoardPosition.MAX - 2);
                    break;
                case PieceType.Rook:
                    position.x = index == 0 ? 0 : (BoardPosition.MAX - 1);
                    break;
                case PieceType.Pawn:
                    position.x = index;
                    position.y = 1;
                    break;
            }

            if (PieceColor.Black == color)
            {
                position.y = BoardPosition.MAX - 1;
                if (type == PieceType.Pawn)
                {
                    position.y -= 1;
                }
            }

            return position;
        }
    }
}
