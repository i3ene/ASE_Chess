using Logic.Pieces;

namespace Logic.Communications.Actions
{
    public class SynchronisationAction : Action
    {
        /// <summary>
        /// Specifies own player role.
        /// </summary>
        public PieceColor? role {  get; set; }

        /// <summary>
        /// Specifies which colors turn it is.
        /// </summary>
        public PieceColor turn { get; set; }

        /// <summary>
        /// All current pieces on the game board.
        /// </summary>
        public Piece[] pieces {  get; set; }

        public SynchronisationAction() : this(null, PieceColor.White, []) { }

        public SynchronisationAction(PieceColor? role, PieceColor turn, Piece[] pieces) : base(ActionType.Synchronisation)
        {
            this.role = role;
            this.turn = turn;
            this.pieces = pieces;
        }
    }
}
