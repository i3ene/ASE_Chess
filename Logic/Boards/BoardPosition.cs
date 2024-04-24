using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Boards
{
    public class BoardPosition : Vector2D
    {
        public const int CHAR_OFFSET = 64;
        public const int MAX = 8;


        public int _x;
        public override int x { get => GetX(); set => SetX(value); }

        public int _y;
        public override int y { get => GetY(); set => SetY(value); }

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
            _x = x % MAX;
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int SetY(int y)
        {
            _y = y % MAX;
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

        public override BoardPosition Clone()
        {
            return new BoardPosition(x, y);
        }
    }
}
