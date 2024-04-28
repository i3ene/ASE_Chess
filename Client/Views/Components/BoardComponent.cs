using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Logic;
using Logic.Boards;
using Logic.Pieces;

namespace Client.Views.Components
{
    public class BoardComponent : Component
    {
        private const int CHESS_UNICODE_OFFSET = 0x2654;

        private readonly ClientGame game;
        private new readonly StyleSize size;

        private BoardPosition? sourcePosition;
        private BoardPosition? targetPosition;

        public BoardComponent(ClientGame game) : base()
        {
            this.game = game;
            size = base.size;
            size.width = new StyleValue(StyleUnit.Fixed, 12);
            size.height = new StyleValue(StyleUnit.Fixed, 12);
            border.style = BorderStyle.Default;
        }

        public override ContentCanvas GetCanvas(ContentCanvas canvas)
        {
            canvas = GetBoard();
            canvas = AddBoardLabels(canvas);
            canvas = AdjustBoardToPlayerView(canvas);
            return canvas;
        }

        private ContentCanvas GetBoard()
        {
            List<ContentString> rows = new List<ContentString>();

            Piece? sourcePiece = sourcePosition == null ? null : game.board.GetPiece(sourcePosition);

            for (int y = 0; y < BoardPosition.MAX; y++)
            {
                ContentString row = new ContentString();
                for (int x = 0; x < BoardPosition.MAX; x++)
                {
                    BoardPosition position = new BoardPosition(x, y);
                    Piece? piece = game.board.GetPiece(position);
                    ContentCharacter field = piece == null ? new ContentCharacter(' ') : GetPieceCharacter(piece);
                    bool isWhiteSquare = (x + y) % 2 == 0;
                    field.Background(isWhiteSquare ? ContentColor.GRAY : ContentColor.BLACK);
                    if (sourcePosition == position) field.Background(targetPosition == null ? ContentColor.ORANGE : ContentColor.PURPLE);
                    if (sourcePiece != null)
                    {
                        bool possible = game.boardService.IsMovePossible(sourcePiece, position);
                        if (possible) field.Background(piece == null ? ContentColor.GREEN : ContentColor.YELLOW);
                    }
                    if (targetPosition == position) field.Background(ContentColor.ORANGE);
                    row.Add(new ContentString([field]));
                }
                rows.Add(row);
            }

            return canvasHelper.ToCanvas(rows.ToArray());
        }

        private ContentCanvas AddBoardLabels(ContentCanvas canvas)
        {
            List<ContentString> rows = new List<ContentString>(canvas.GetRows());

            ContentString letterText = new ContentString(new string(' ', 2));
            int[] numbers = new int[BoardPosition.MAX].Select((c, i) => (i + 1)).ToArray();
            char[] numberChars = numbers.Select(i => i.ToString()[0]).ToArray();
            char[] letterChars = numbers.Select(i => (char)(i - 1 + BoardPosition.CHAR_OFFSET)).ToArray();
            string letterString = new string(letterChars);
            letterText.Insert(1, letterString);

            for (int i = 0; i < BoardPosition.MAX; i++)
            {
                string number = numberChars[i].ToString();
                rows[i].Insert(0, number);
                rows[i].Add(number);
            }
            rows.Insert(0, letterText);
            rows.Add(letterText);

            return canvasHelper.ToCanvas(rows.ToArray());
        }

        private ContentCanvas AdjustBoardToPlayerView(ContentCanvas canvas)
        {
            if (game.GetOwnColor() == PieceColor.Black) return canvas;
            ContentString[] reversedRows = canvas.GetRows().Reverse().ToArray();
            return canvasHelper.ToCanvas(reversedRows);
        }

        private ContentCharacter GetPieceCharacter(Piece piece)
        {
            Settings settings = Settings.getInstance();
            ContentCharacter symbol = settings.IsChessSymbolDisplayed() ? GetPieceSymbol(piece) : GetPieceLetter(piece);
            symbol.Foreground(piece.color == PieceColor.White ? ContentColor.RED : ContentColor.BLUE);
            return symbol;
        }

        private ContentCharacter GetPieceSymbol(Piece piece)
        {
            int pieces = game.pieceService.GetPieceTypes().ToArray().Length;
            int code = CHESS_UNICODE_OFFSET + (int)piece.type + (int)piece.color * pieces;
            ContentCharacter symbol = new ContentCharacter((char)code);
            return symbol;
        }

        private ContentCharacter GetPieceLetter(Piece piece)
        {
            char character = ' ';
            switch (piece.type)
            {
                case PieceType.King:
                    character = 'K';
                    break;
                case PieceType.Queen:
                    character = 'Q';
                    break;
                case PieceType.Pawn:
                    character = 'P';
                    break;
                case PieceType.Rook:
                    character = 'R';
                    break;
                case PieceType.Knight:
                    character = 'N';
                    break;
                case PieceType.Bishop:
                    character = 'B';
                    break;
            }
            ContentCharacter letter = new ContentCharacter(character);
            return letter;
        }

        public BoardPosition? GetSourcePosition()
        {
            return sourcePosition?.Clone();
        }

        public void SetSourcePosition(BoardPosition? position)
        {
            sourcePosition = position;
            Update();
        }

        public BoardPosition? GetTargetPosition()
        {
            return targetPosition?.Clone();
        }

        public void SetTargetPosition(BoardPosition? position)
        {
            targetPosition = position;
            Update();
        }
    }
}
