using Client.Views.Components;
using Client.Views.Components.Styles.Borders;
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
    public class GameView : View, IInteractable
    {
        private readonly Game game;
        private readonly BoardComponent board;
        private readonly TextComponent info;

        public GameView(ViewRouter router, Game game) : base(router)
        {
            this.game = game;

            board = new BoardComponent(game);
            board.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            AddChild(board);

            info = new TextComponent();
            info.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            info.size.width = 100;
            info.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            info.position.yUnit = Components.Styles.ComponentUnit.Auto;
            info.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            UpdateInfoText();
        }

        public void HandleInteraction(InteractionArgument args)
        {
            switch (args.key.Key)
            {
                case ConsoleKey.Escape:
                    HandleInteractionEscape(args);
                    break;
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
            UpdateInfoText();
        }

        private void HandleInteractionEscape(InteractionArgument args)
        {
            args.handled = true;
            // TODO
            MenuView menu = new MenuView(router);
            router.Display(menu);
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

        private void UpdateInfoText()
        {
            ContentString text = new ContentString();
            text.Add("Press ");
            text.Add(new ContentString("[ESC]").Background(ContentColor.PURPLE));
            text.Add(" to quit.");

            text.Add("\nPress ");
            text.Add(new ContentString("[Enter]").Background(ContentColor.PURPLE));
            if (board.GetTargetPosition() != null)
            {
                text.Add(" to confirm target position.");
            }
            else if (board.GetSourcePosition() != null)
            {
                text.Add(" to confirm source position.");
            }
            else
            {
                text.Add(" to enable cursor.");
            }

            if (board.GetTargetPosition() != null || board.GetSourcePosition() != null)
            {
                text.Add("\nPress ");
                text.Add(new ContentString("[←]").Background(ContentColor.PURPLE));
                text.Add(", ");
                text.Add(new ContentString("[↑]").Background(ContentColor.PURPLE));
                text.Add(", ");
                text.Add(new ContentString("[↓]").Background(ContentColor.PURPLE));
                text.Add(", ");
                text.Add(new ContentString("[→]").Background(ContentColor.PURPLE));
                text.Add(" to change selection.");

                text.Add("\nPress ");
                text.Add(new ContentString("[Delete]").Background(ContentColor.PURPLE));
                text.Add(" to cancel current action.");
            }

            info.SetText(text);
        }
    }
}
