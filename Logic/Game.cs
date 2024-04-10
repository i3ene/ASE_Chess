using Logic.Boards;
using Logic.Pieces;
using Logic.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Game
    {
        public readonly PieceRepository board;
        private readonly BoardService boardService;
        public readonly PlayerRepository players;
        public PieceColor currentColor;

        public Game()
        {
            board = new PieceRepository();
            boardService = new BoardService(board);
            players = new PlayerRepository();

            PieceService pieceService = new PieceService();
            PieceFactory pieceFactory = new PieceFactory(pieceService);
            board.AddPieces(pieceFactory.CreatePieces().ToArray());

            PlayerFactory playerFactory = new PlayerFactory(pieceService);
            players.AddPlayers(playerFactory.CreatePlayers().ToArray());

            currentColor = PieceColor.White;
        }

        public void Turn()
        {
            currentColor = PieceColor.White == currentColor ? PieceColor.Black : PieceColor.White;
        }

        public bool Move(BoardPosition source, BoardPosition target)
        {
            Piece? piece = board.GetPiece(source);
            if (piece is null) return false;
            if (piece.color != currentColor) return false;
            return boardService.Move(piece, target);
        }

    }
}
