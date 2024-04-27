using Logic.Pieces;

namespace Logic.Communications.Actions
{
    public class ParticipationAction : Action
    {
        /// <summary>
        /// How to participate.
        /// </summary>
        public ParticipationType participation {  get; set; }

        /// <summary>
        /// Specifies which color is preferred or was assigned.
        /// </summary>
        public PieceColor? color { get; set; }

        public ParticipationAction() : this(ParticipationType.View) { }

        public ParticipationAction(PieceColor color) : this(ParticipationType.Join)
        {
            this.color = color;
        }

        public ParticipationAction(ParticipationType participation) : base(ActionType.Participation)
        {
            this.participation = participation;
        }
    }
}
