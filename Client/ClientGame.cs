using Client.Communications;
using Logic;
using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class ClientGame : Game
    {
        private PieceColor? color;
        private readonly ClientSocket<Logic.Communications.Actions.Action> socket;

        public ClientGame(ClientSocket<Logic.Communications.Actions.Action> socket) : base()
        {
            this.socket = socket;

            this.socket.Connected += SocketConnected;
            this.socket.Data += SocketData;
        }

        private void SocketConnected()
        {
            socket.Send(new SynchronisationAction());
        }

        private void SocketData(Logic.Communications.Actions.Action data)
        {
            switch (data.type)
            {
                case ActionType.Participation:
                    HandleParticipation((ParticipationAction)data);
                    break;
                case ActionType.Turn:
                    HandleTurn((TurnAction)data);
                    break;
                case ActionType.Move:
                    HandleMove((MoveAction)data);
                    break;
                case ActionType.Synchronisation:
                    HandleSynchronisation((SynchronisationAction)data);
                    break;
            }
        }

        private void HandleParticipation(ParticipationAction participation)
        {
            color = participation.color;
        }

        private void HandleTurn(TurnAction turn)
        {
            base.Turn();
        }

        private void HandleMove(MoveAction move)
        {
            base.Move(BoardPosition.FromNotation(move.sourcePosition), BoardPosition.FromNotation(move.targetPosition));
        }

        private void HandleSynchronisation(SynchronisationAction sync)
        {
            color = sync.role;
            board.RemoveAllPieces();
            board.AddPieces(sync.pieces);
            currentColor = sync.turn;
        }

        public bool IsOnTurn()
        {
            return !(color is null || color != currentColor);
        }

        public new void Turn()
        {
            if (!IsOnTurn()) return;
            socket.Send(new TurnAction());
        }

        public new void Move(BoardPosition source, BoardPosition target)
        {
            if (!IsOnTurn()) return;
            socket.Send(new MoveAction(source.GetNotation(), target.GetNotation()));
        }
    }
}
