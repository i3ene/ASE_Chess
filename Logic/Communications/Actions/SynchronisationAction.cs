using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Communications.Actions
{
    public class SynchronisationAction : Action
    {
        /// <summary>
        /// Specifies which colors turn it is.
        /// </summary>
        public PieceColor color { get; set; }

        /// <summary>
        /// All current pieces on the game board.
        /// </summary>
        public Piece[] pieces {  get; set; }

        public SynchronisationAction() : this(PieceColor.White, []) { }

        public SynchronisationAction(PieceColor color, Piece[] pieces) : base(ActionType.Synchronisation)
        {
            this.color = color;
            this.pieces = pieces;
        }
    }
}
