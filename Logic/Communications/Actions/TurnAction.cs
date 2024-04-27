using Logic.Pieces;

namespace Logic.Communications.Actions
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
