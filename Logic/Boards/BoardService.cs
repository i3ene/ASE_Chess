using Logic.Pieces;

namespace Logic.Boards
{
    public class BoardService
    {
        public readonly Board board;

        public BoardService(Board board)
        {
            this.board = board;
        }

        public bool Move(Piece piece, BoardPosition position)
        {
            bool isPossible = IsMovePossible(piece, position);
            if (!isPossible) return false;
            piece.position.Set(position);
            return true;
        }

        public bool IsMovePossible(Piece piece, BoardPosition position)
        {
            // Cannot move to current position
            if (piece.position == position) return false;

            // Check if position is already occupied by another Piece of same color
            Piece? targetPiece = board.GetPiece(position);
            if (targetPiece != null && targetPiece.color == piece.color) return false;

            switch (piece.type)
            {
                case PieceType.King:
                    return IsKingMovePossible(piece, position);
                case PieceType.Queen:
                    return IsQueenMovePossible(piece, position);
                case PieceType.Bishop:
                    return IsBishopMovePossible(piece, position);
                case PieceType.Knight:
                    return IsKnightMovePossible(piece, position);
                case PieceType.Rook:
                    return IsRookMovePossible(piece, position);
                case PieceType.Pawn:
                    return IsPawnMovePossible(piece, position);
            }
            return false;
        }

        private bool IsPawnMovePossible(Piece piece, BoardPosition position)
        {
            int x = position.x - piece.position.x;
            int y = position.y - piece.position.y;

            // Check which direction Pawn can move
            int direction = (piece.color == PieceColor.White ? 1 : -1);
            // Check if Pawn is in starting position
            int length = (piece.color == PieceColor.White ? piece.position.y == 1 : piece.position.y == (BoardPosition.MAX - 1)) ? 2 : 1;
            // Check if Pawn is moving forward
            bool possible = piece.color == PieceColor.White ? y >= 0 : y <= 0;
            possible &= Math.Abs(y) <= length;
            possible &= x == 0;
            possible &= !IsPathBlocked(piece, position);
            possible &= !IsPositionOccupied(position);
            // Check if Pawn is capturing a Piece
            possible |= (Math.Abs(x) == 1 && y == direction && IsPieceCapturing(piece, position));

            return possible;
        }

        private bool IsRookMovePossible(Piece piece, BoardPosition position)
        {
            int ax = Math.Abs(position.x - piece.position.x);
            int ay = Math.Abs(position.y - piece.position.y);

            bool possible = (ax == 0 || ay == 0);
            possible &= !IsPathBlocked(piece, position);

            return possible;
        }

        private bool IsKnightMovePossible(Piece piece, BoardPosition position)
        {
            int ax = Math.Abs(position.x - piece.position.x);
            int ay = Math.Abs(position.y - piece.position.y);

            bool possible = ((ax == 2 && ay == 1) || (ay == 2 && ax == 1));

            return possible;
        }

        private bool IsBishopMovePossible(Piece piece, BoardPosition position)
        {
            int ax = Math.Abs(piece.position.x - position.x);
            int ay = Math.Abs(piece.position.y - position.y);

            bool possible = (ax == ay);
            possible &= !IsPathBlocked(piece, position);

            return possible;
        }

        private bool IsKingMovePossible(Piece piece, BoardPosition position)
        {
            int ax = Math.Abs(position.x - piece.position.x);
            int ay = Math.Abs(position.y - piece.position.y);

            bool possible = (ax <= 1 && ay <= 1);
            possible &= !IsPathBlocked(piece, position);
            // Check if King is getting "checked" in this position
            possible &= !SetsCheck(piece, position);
            return possible;
        }

        public bool IsQueenMovePossible(Piece piece, BoardPosition position)
        {
            int ax = Math.Abs(position.x - piece.position.x);
            int ay = Math.Abs(position.y - piece.position.y);

            bool possible = (ax == 0 || ay == 0 || ax == ay);
            possible &= !IsPathBlocked(piece, position);

            return possible;
        }

        public bool IsPositionOccupied(BoardPosition position)
        {
            Piece? piece = board.GetPiece(position);
            return piece != null;
        }

        public bool IsPieceCapturing(Piece piece, BoardPosition position)
        {
            Piece? capturing = board.GetPiece(position);
            if (capturing is null) return false;
            return capturing.color != piece.color;
        }

        public bool IsPathBlocked(Piece piece, BoardPosition position)
        {
            int x = position.x - piece.position.x;
            int y = position.y - piece.position.y;

            BoardPosition step = new BoardPosition(0, 0);
            if (x == 0)
            {
                step.y = y > 0 ? 1 : -1;
            }
            else if (y == 0)
            {
                step.x = x > 0 ? 1 : -1;
            }
            else if (Math.Abs(x) == Math.Abs(y))
            {
                step.x = x > 0 ? 1 : -1;
                step.y = y > 0 ? 1 : -1;
            }
            int steps = Math.Max(Math.Abs(x), Math.Abs(y));
            for (int i = 1; i < steps; i++)
            {
                var checkPosition = new BoardPosition(piece.position.x + (step.x * i), piece.position.y + (step.y * i));
                if (IsPositionOccupied(checkPosition)) return true;
            }
            return false;
        }

        public bool SetsCheck(Piece piece, BoardPosition position)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Checks if a color is in check.
        /// </summary>
        /// <returns><see cref="PieceColor"/> of player in check or <see langword="null"/> if no check is present.</returns>
        public PieceColor? IsCheck()
        {
            // TODO
            return null;
        }
    }
}
