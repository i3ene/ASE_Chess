using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Games
{
    public interface IGameInteraction
    {
        public event EventHandler<TurnAction> Turn;
        public event EventHandler<MoveAction> Move;
        public event EventHandler<SynchronisationAction> RequestSynchronisation;

        public void AfterTurn(PieceColor color);
        public void AfterMove(BoardPosition source, BoardPosition target);
        public void OnSynchronisation(PieceColor color, Piece[] pieces);
    }
}
