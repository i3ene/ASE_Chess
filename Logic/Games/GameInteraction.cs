using Logic.Boards;
using Logic.Communications.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Games
{
    public class GameInteraction
    {
        private readonly IGameInteraction interaction;
        private readonly Game game;

        public GameInteraction(IGameInteraction interaction, Game game)
        {
            this.interaction = interaction;
            this.game = game;

            this.interaction.Turn += InteractionTurn;
            this.interaction.Move += InteractionMove;
            this.interaction.RequestSynchronisation += InteractionRequestSynchronisation;
        }

        private void InteractionRequestSynchronisation(object? sender, SynchronisationAction e)
        {
            interaction.OnSynchronisation(game.currentColor, game.board.GetAllPieces().ToArray());
        }

        private void InteractionMove(object? sender, MoveAction e)
        {
            BoardPosition source = BoardPosition.FromNotation(e.sourcePosition);
            BoardPosition target = BoardPosition.FromNotation(e.targetPosition);
            bool success = game.Move(source, target);
            if (!success) return;
            interaction.AfterMove(source, target);
        }

        private void InteractionTurn(object? sender, TurnAction e)
        {
            game.Turn();
            interaction.AfterTurn(game.currentColor);
        }

    }
}
