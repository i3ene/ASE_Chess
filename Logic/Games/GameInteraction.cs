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
            this.interaction.Synchronisation += InteractionSynchronisation;
        }

        private void InteractionSynchronisation(object? sender, GameInteractionArguments<SynchronisationAction> e)
        {
            game.currentColor = e.action.color;
            game.board.RemoveAllPieces();
            game.board.AddPieces(e.action.pieces);
            e.handled = true;
        }

        private void InteractionMove(object? sender, GameInteractionArguments<MoveAction> e)
        {
            if (e.actor != game.currentColor) return;
            BoardPosition source = BoardPosition.FromNotation(e.action.sourcePosition);
            BoardPosition target = BoardPosition.FromNotation(e.action.targetPosition);
            bool success = game.Move(source, target);
            e.handled = success;
        }

        private void InteractionTurn(object? sender, GameInteractionArguments<TurnAction> e)
        {
            if (e.actor != game.currentColor) return;
            game.Turn();
            e.action.color = game.currentColor;
            e.handled = true;
        }

    }
}
