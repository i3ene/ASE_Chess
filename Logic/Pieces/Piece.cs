using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Pieces
{
    public class Piece : ICloneable
    {
        public readonly PieceColor color;
        public readonly PieceType type;
        public readonly BoardPosition position;

        public Piece(PieceColor color, PieceType type, BoardPosition position)
        {
            this.color = color;
            this.type = type;
            this.position = position;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
