using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Pieces
{
    public class Piece : ICloneable
    {
        public readonly PieceType type;
        public readonly PieceColor color;
        public readonly BoardPosition position;

        public Piece(PieceType type, PieceColor color, BoardPosition position)
        {
            this.type = type;
            this.color = color;
            this.position = position;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
