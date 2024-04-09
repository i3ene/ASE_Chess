using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Players
{
    public class Player
    {
        public readonly PieceColor color;

        public Player(PieceColor color)
        {
            this.color = color;
        }
    }
}
