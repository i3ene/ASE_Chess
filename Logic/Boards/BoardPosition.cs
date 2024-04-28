namespace Logic.Boards
{
    public class BoardPosition : ICloneable
    {
        public const int CHAR_OFFSET = 65;
        public const int MAX = 8;


        public int _x;
        public int x { get => GetX(); set => SetX(value); }

        public int _y;
        public int y { get => GetY(); set => SetY(value); }

        public char cx { get => GetCharX(); set => SetCharX(value); }

        public BoardPosition() : base() { }

        public BoardPosition(int x, int y) : base()
        {
            SetX(x);
            SetY(y);
        }

        public BoardPosition(char x, int y) : this(CharToInt(x), y) { }

        public int GetX()
        {
            return _x;
        }

        public int SetX(int x)
        {
            x = x % MAX;
            if (x < 0) x = MAX + x;
            _x = x;
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int SetY(int y)
        {
            y = y % MAX;
            if (y < 0) y = MAX + y;
            _y = y;
            return _y;
        }

        public char GetCharX()
        {
            return IntToChar(GetX());
        }

        public int SetCharX(char x)
        {
            return SetX(CharToInt(x));
        }

        public string GetNotation()
        {
            return $"{cx}{y}";
        }

        public static BoardPosition FromNotation(string notation)
        {
            BoardPosition position = new BoardPosition();
            notation = notation.Trim();
            if (notation.Length < 2) notation = "A1";
            notation = notation.ToUpper();

            if (!char.IsLetter(notation[0])) notation = "A" + notation[1];
            position.x = CharToInt(notation[0]);

            if (!char.IsDigit(notation[1])) notation = notation[0] + "1";
            position.y = int.Parse(notation[1].ToString());

            return position;
        }

        public static char IntToChar(int i)
        {
            return (char)(CHAR_OFFSET + i);
        }

        public static int CharToInt(char c)
        {
            return char.ToUpper(c) - CHAR_OFFSET;
        }

        public BoardPosition Set(BoardPosition other)
        {
            x = other.x;
            y = other.y;
            return this;
        }

        public BoardPosition Add(BoardPosition other)
        {
            x += other.x;
            y += other.y;
            return this;
        }

        public BoardPosition Subtract(BoardPosition other)
        {
            x -= other.x;
            y -= other.y;
            return this;
        }

        public BoardPosition Multiply(BoardPosition other)
        {
            x *= other.x;
            y *= other.y;
            return this;
        }

        public BoardPosition Divide(BoardPosition other)
        {
            x /= other.x;
            y /= other.y;
            return this;
        }

        public static BoardPosition operator +(BoardPosition lhs, BoardPosition rhs)
        {
            return lhs.Clone().Add(rhs);
        }

        public static BoardPosition operator -(BoardPosition lhs, BoardPosition rhs)
        {
            return lhs.Clone().Subtract(rhs);
        }

        public static BoardPosition operator *(BoardPosition lhs, BoardPosition rhs)
        {
            return lhs.Clone().Multiply(rhs);
        }

        public static BoardPosition operator /(BoardPosition lhs, BoardPosition rhs)
        {
            return lhs.Clone().Divide(rhs);
        }

        public bool Equals(BoardPosition? v)
        {
            if (v is null)
            {
                return false;
            }

            if (ReferenceEquals(this, v))
            {
                return true;
            }

            if (GetType() != v.GetType())
            {
                return false;
            }

            return x == v.x && y == v.y;
        }

        public static bool operator ==(BoardPosition? lhs, BoardPosition? rhs)
        {
            if (lhs is null)
            {
                return rhs is null;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(BoardPosition? lhs, BoardPosition? rhs) => !(lhs == rhs);

        public override bool Equals(object? obj)
        {
            return obj is BoardPosition && Equals((BoardPosition)obj);
        }

        public override int GetHashCode() => (x, y).GetHashCode();

        object ICloneable.Clone()
        {
            return Clone();
        }

        public BoardPosition Clone()
        {
            return new BoardPosition(x, y);
        }
    }
}
