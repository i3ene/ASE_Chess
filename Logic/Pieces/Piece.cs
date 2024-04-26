using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Boards;

namespace Logic.Pieces
{
    public class Piece : ICloneable, IEquatable<Piece>
    {
        public readonly Guid id;
        public readonly PieceColor color;
        public readonly PieceType type;
        public readonly BoardPosition position;

        public Piece(PieceColor color, PieceType type, BoardPosition position)
        {
            id = Guid.NewGuid();
            this.color = color;
            this.type = type;
            this.position = position;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Piece? other)
        {
            return id == other?.id;
        }
    }
}
