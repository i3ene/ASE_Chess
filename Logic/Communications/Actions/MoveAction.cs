namespace Logic.Communications.Actions
{
    public class MoveAction : Action
    {
        /// <summary>
        /// From which field to move a (possible) piece.
        /// </summary>
        /// <remarks>
        /// This should be in chess notation.
        /// </remarks>
        public string sourcePosition {  get; set; }

        /// <summary>
        /// To which field to move a (possible) piece to.
        /// </summary>
        /// <remarks>
        /// This should be in chess notation.
        /// </remarks>
        public string targetPosition { get; set; }

        public MoveAction() : this("", "") { }

        public MoveAction(string sourcePosition, string targetPosition) : base(ActionType.Move)
        {
            this.sourcePosition = sourcePosition;
            this.targetPosition = targetPosition;
        }
    }
}
