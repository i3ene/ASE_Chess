using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BoardPosition : Vector2D
    {
        public const int CHAR_OFFSET = 64;
        public const int MAX = 8;


        public int _x;
        public new int x { get => getX(); set => setX(value); }

        public int _y;
        public new int y { get => getY(); set => setY(value); }

        public char cx { get => getCharX(); set => setCharX(value); }

        public BoardPosition() : base() { }

        public BoardPosition(int x, int y) : base(x, y) { }

        public BoardPosition(char x, int y) : base(charToInt(x), y) { }

        public int getX()
        {
            return _x;
        }

        public int setX(int x)
        {
            _x = x % MAX;
            return _x;
        }

        public int getY()
        {
            return _y;
        }

        public int setY(int y)
        {
            _y = y % MAX;
            return _y;
        }

        public char getCharX()
        {
            return intToChar(getX());
        }

        public int setCharX(char x)
        {
            return setX(charToInt(x));
        }

        public string getNotation()
        {
            return $"{cx}{y}";
        }

        public static BoardPosition fromNotation(string notation)
        {
            BoardPosition position = new BoardPosition();
            notation = notation.Trim();
            if (notation.Length < 2) notation = "A1";
            notation = notation.ToUpper();

            if (!char.IsLetter(notation[0])) notation = "A" + notation[1];
            position.x = charToInt(notation[0]);

            if (!char.IsDigit(notation[1])) notation = notation[0] + "1";
            position.y = int.Parse(notation[1].ToString());

            return position;
        }

        public static char intToChar(int i)
        {
            return (char)(CHAR_OFFSET + i);
        }

        public static int charToInt(char c)
        {
            return char.ToUpper(c) - CHAR_OFFSET;
        }

        public new BoardPosition Clone()
        {
            return new BoardPosition(x, y);
        }
    }
}
