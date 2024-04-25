using Client.Views.Components;
using Client.Views.Contents;
using Client.Views.Interactions;
using Logic;
using Logic.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class GameView : View, IInteraction
    {
        private readonly Game game;
        private readonly BoardComponent board;

        public GameView(ViewRouter router, Game game) : base(router)
        {
            this.game = game;

            board = new BoardComponent(game);
            board.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            AddChild(board);
        }

        public void HandleInteraction(InteractionArgument args)
        {
            switch (args.key.Key)
            {
                case ConsoleKey.Delete:
                    HandleInteractionDelete(args);
                    break;
                case ConsoleKey.Enter:
                    HandleInteractionEnter(args);
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    HandleInteractionArrow(args);
                    break;
            }
        }

        private void HandleInteractionDelete(InteractionArgument args)
        {
            args.handled = true;
            if (board.GetTargetPosition() == null)
            {
                board.SetSourcePosition(null);
            }
            board.SetTargetPosition(null);
        }

        private void HandleInteractionEnter(InteractionArgument args)
        {
            args.handled = true;
            if (board.GetSourcePosition() == null)
            {
                board.SetSourcePosition(new BoardPosition());
                return;
            }
            if (board.GetTargetPosition() == null)
            {
                board.SetTargetPosition(board.GetSourcePosition());
                return;
            }
            // TODO: Send move action to game/server
        }

        private void HandleInteractionArrow(InteractionArgument args)
        {
            args.handled = true;
            BoardPosition? currentPosition = null;
            if (board.GetTargetPosition() != null)
            {
                currentPosition = board.GetTargetPosition();
            }
            else
            {
                currentPosition = board.GetSourcePosition();
            }
            if (currentPosition == null) return;

            switch (args.key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentPosition.y -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    currentPosition.y += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    currentPosition.x -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    currentPosition.x += 1;
                    break;
            }

            if (board.GetTargetPosition() != null)
            {
                board.SetTargetPosition(currentPosition);
            }
            else
            {
                board.SetSourcePosition(currentPosition);
            }
        }
    }
}
