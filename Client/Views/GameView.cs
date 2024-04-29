using System.Net.WebSockets;
using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Client.Views.Interactions;
using Logic;
using Logic.Boards;
using Logic.Pieces;

namespace Client.Views
{
    public class GameView : View, IInteractable
    {
        private readonly ClientGame game;
        private readonly BoardComponent board;
        private readonly TextComponent info;
        private readonly DialogComponent dialog;
        private readonly TextComponent turnText;
        private readonly TextComponent participationText;

        public GameView(ViewRouter router, ClientGame game) : base(router)
        {
            this.game = game;

            turnText = new TextComponent();
            turnText.size.width = new StyleValue(StyleUnit.Auto);
            turnText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            turnText.position.y = new StyleValue(StyleUnit.Auto);
            AddChild(turnText);

            ComponentContainer boardContainer = new ComponentContainer();
            boardContainer.size.height = new StyleValue(StyleUnit.Auto);
            boardContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            boardContainer.position.y = new StyleValue(StyleUnit.Auto);
            AddChild(boardContainer);

            board = new BoardComponent(game);
            board.position.y = new StyleValue(StyleUnit.Auto);
            board.alignment.horizontal = HorizontalAlignment.Center;
            board.alignment.vertical = VerticalAlignment.Middle;
            boardContainer.AddChild(board);

            info = new TextComponent();
            info.size.width = new StyleValue(StyleUnit.Relative, 100);
            info.size.height = new StyleValue(StyleUnit.Auto);
            info.position.y = new StyleValue(StyleUnit.Auto);
            info.alignment.vertical = VerticalAlignment.Bottom;
            info.textAlignment.horizontal = HorizontalAlignment.Center;
            info.border.positions = [BorderPosition.Top];
            info.border.style = BorderStyle.Thin;
            AddChild(info);

            participationText = new TextComponent();
            participationText.size.width = new StyleValue(StyleUnit.Auto);
            participationText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            participationText.position.y = new StyleValue(StyleUnit.Auto);
            participationText.alignment.vertical = VerticalAlignment.Bottom;
            participationText.alignment.horizontal = HorizontalAlignment.Right;
            AddChild(participationText);

            dialog = new DialogComponent();
            dialog.size.width = new StyleValue(StyleUnit.Auto);
            //dialog.position.y = new StyleValue(StyleUnit.Fixed, 0);
            dialog.alignment.horizontal = HorizontalAlignment.Center;
            dialog.alignment.vertical = VerticalAlignment.Middle;
            dialog.border.style = BorderStyle.Thin;
            dialog.SetText("Are you sure you want to quit?");
            dialog.SetActions(["Quit", "Cancel"]);
            dialog.OnAction += HandleDialogAction;

            game.OnChange += UpdateDisplay;
            UpdateDisplay();
        }

        private ContentString GetColorText(PieceColor? color) => color switch
        {
            PieceColor.White => new ContentString("White").Foreground(ContentColor.RED),
            PieceColor.Black => new ContentString("Black").Foreground(ContentColor.BLUE),
            _ => new ContentString("View")
        };

        private void UpdateDisplay()
        {
            if (!game.IsOnTurn())
            {
                board.SetSourcePosition(null);
                board.SetTargetPosition(null);
            }

            ContentString participation = GetColorText(game.GetOwnColor());
            participation = "(" + participation + ")";
            participationText.SetText(participation);

            ContentString turn = GetColorText(game.currentColor);
            turn = "Current: " + turn;
            turnText.SetText(turn);

            UpdateInfoText();
        }

        private void HandleDialogAction(DialogComponent sender, string action)
        {
            if (action == "Quit")
            {
                game.socket.Close();
                ViewMenu();
                return;
            }
            RemoveChild(dialog);
        }

        private void ViewMenu()
        {
            MenuView menu = new MenuView(router);
            router.Display(menu);
        }

        public void HandleInteraction(InteractionArgument args)
        {
            if (IsDialogOpen())
            {
                args.sender.InvokeInteractionEvent(dialog, args);
                args.handled = true;
                return;
            }

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
            AddChild(dialog);
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

            if (!game.IsOnTurn()) return;

            BoardPosition? sourcePosition = board.GetSourcePosition();
            if (sourcePosition == null)
            {
                int yPosition = game.GetOwnColor() == PieceColor.Black ? (BoardPosition.MAX - 1) : 0;
                board.SetSourcePosition(new BoardPosition(0, yPosition));
                return;
            }
            BoardPosition? targetPosition = board.GetTargetPosition();
            if (targetPosition == null)
            {
                board.SetTargetPosition(board.GetSourcePosition());
                return;
            }
            game.Move(sourcePosition, targetPosition);
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

            int yDirection = game.GetOwnColor() == PieceColor.Black ? 1 : -1;

            switch (args.key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentPosition.y -= yDirection;
                    break;
                case ConsoleKey.DownArrow:
                    currentPosition.y += yDirection;
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
            text += "Press ";
            text += new ContentString("[ESC]").Background(ContentColor.PURPLE);
            text += " to quit.";

            if (game.GetOwnColor() == null)
            {
                text += "\nYou are currently viewing a game";
            }
            else if (game.IsOnTurn())
            {
                text += "\nPress ";
                text += new ContentString("[Enter]").Background(ContentColor.PURPLE);
                if (board.GetTargetPosition() != null)
                {
                    text += " to confirm target position.";
                }
                else if (board.GetSourcePosition() != null)
                {
                    text += " to confirm source position.";
                }
                else
                {
                    text += " to enable cursor.";
                }
            }
            else
            {
                text += "\nOpponent is on turn, please wait...";
            }

            if (board.GetTargetPosition() != null || board.GetSourcePosition() != null)
            {
                text += "\nPress ";
                text += new ContentString("[←]").Background(ContentColor.PURPLE);
                text += ", ";
                text += new ContentString("[↑]").Background(ContentColor.PURPLE);
                text += ", ";
                text += new ContentString("[↓]").Background(ContentColor.PURPLE);
                text += ", ";
                text += new ContentString("[→]").Background(ContentColor.PURPLE);
                text += " to change selection.";

                text += "\nPress ";
                text += new ContentString("[Delete]").Background(ContentColor.PURPLE);
                text += " to cancel current action.";
            }

            info.SetText(text);
        }

        private bool IsDialogOpen()
        {
            foreach (Component child in GetAllChilds())
            {
                if (child == dialog) return true;
            }
            return false;
        }
    }
}
