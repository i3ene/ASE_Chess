using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Communication.Actions
{
    public class TurnAction : Action
    {
        /// <summary>
        /// Specifies which colors turn it is now.
        /// </summary>
        public PieceColor color {  get; set; }

        public TurnAction() : this(PieceColor.White) { }

        public TurnAction(PieceColor color) : base(ActionType.Turn)
        {
            this.color = color;
        }
    }
}
