using Client.Communication;
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
    public class ClientGame
    {
        private PieceColor? color;
        private readonly ClientSocket<Logic.Communications.Actions.Action> socket;
        private readonly Game game;

        public ClientGame(ClientSocket<Logic.Communications.Actions.Action> socket, Game game)
        {
            this.socket = socket;
            this.game = game;

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
            game.Turn();
        }

        private void HandleMove(MoveAction move)
        {
            game.Move(BoardPosition.FromNotation(move.sourcePosition), BoardPosition.FromNotation(move.targetPosition));
        }

        private void HandleSynchronisation(SynchronisationAction sync)
        {
            color = sync.role;
            game.board.RemoveAllPieces();
            game.board.AddPieces(sync.pieces);
            game.currentColor = sync.turn;
        }

        public bool IsOnTurn()
        {
            return !(color is null || color != game.currentColor);
        }

        public void Turn()
        {
            if (!IsOnTurn()) return;
            socket.Send(new TurnAction());
        }

        public void Move(BoardPosition source, BoardPosition target)
        {
            if (!IsOnTurn()) return;
            socket.Send(new MoveAction(source.GetNotation(), target.GetNotation()));
        }
    }
}
