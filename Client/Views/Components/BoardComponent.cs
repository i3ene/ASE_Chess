using Client.Views.Components.Styles;
using Client.Views.Contents;
using Logic;
using Logic.Boards;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class BoardComponent : Component
    {
        private const int CHESS_UNICODE_OFFSET = 0x2654;

        private readonly Game game;
        private new readonly ComponentSize size;

        private bool displayChessSymbols;

        public BoardComponent(Game game) : base()
        {
            this.game = game;
            size = base.size;
            size.widthUnit = ComponentUnit.Fixed;
            size.heightUnit = ComponentUnit.Fixed;
            size.x = 8;
            size.y = 8;
            displayChessSymbols = false;
        }

        public override ContentCanvas GetCanvas(ContentCanvas canvas)
        {
            List<ContentString> rows = new List<ContentString>();

            for (int y = 0; y < BoardPosition.MAX; y++)
            {
                ContentString row = new ContentString();
                for (int x = 0; x < BoardPosition.MAX; x++)
                {
                    Piece? piece = game.board.GetPiece(new BoardPosition(x, y));
                    ContentCharacter field = piece == null ? new ContentCharacter(' ') : GetPieceCharacter(piece);
                    bool isWhiteSquare = (x + y) % 2 == 0;
                    field.Background(isWhiteSquare ? ContentColor.GRAY : ContentColor.BLACK);
                    row.Add(new ContentString([field]));
                }
                rows.Add(row);
            }

            return canvasService.ToCanvas(rows.ToArray());
        }

        private ContentCharacter GetPieceCharacter(Piece piece)
        {
            ContentCharacter symbol = displayChessSymbols ? GetPieceSymbol(piece) : GetPieceLetter(piece);
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
    }
}
