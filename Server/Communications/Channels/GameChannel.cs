using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Pieces;
using Logic.Players;
using Server.Communications;
using Server.Players;
using Action = Logic.Communications.Actions.Action;

namespace Server.Communications.Channels
{
    public class GameChannel : Channel
    {
        private readonly Game game;
        private readonly ServerPlayerRepository players;

        public GameChannel() : base("game")
        {
            game = new Game();
            players = new ServerPlayerRepository();
        }

        public override void RegisterServer(SocketServer<Action> server)
        {
            server.SocketData += ServerSocketData;
        }

        private void ServerSocketData(ServerSocket<Action> socket, Action data)
        {
            switch (data.type)
            {
                case ActionType.Participation:
                    HandleParticipationAction(socket, (ParticipationAction)data);
                    break;
                case ActionType.Move:
                    HandleMoveAction(socket, (MoveAction)data);
                    break;
                case ActionType.Turn:
                    HandleTurnAction(socket, (TurnAction)data);
                    break;
                case ActionType.Synchronisation:
                    HandleSynchronisationAction(socket, (SynchronisationAction)data);
                    break;
            }
        }

        private void SynchroniseSocket(ServerSocket<Action> socket)
        {
            SynchronisationAction sync = new SynchronisationAction(
                players.GetPlayer(socket)?.color,
                game.currentColor,
                game.board.GetAllPieces().ToArray()
            );
            socket.Send(sync);
        }

        private bool IsSocketOnTurn(ServerSocket<Action> socket)
        {
            ServerPlayer? player = players.GetPlayer(socket);
            if (player is null)
            {
                socket.Send(new MessageAction(sender, "Not playing"));
                return false;
            }
            if (player.color != game.currentColor)
            {
                socket.Send(new MessageAction(sender, "Not on turn"));
                return false;
            }
            return true;
        }

        private void HandleSynchronisationAction(ServerSocket<Action> socket, SynchronisationAction synchronisation)
        {
            SynchroniseSocket(socket);
        }

        private void HandleMoveAction(ServerSocket<Action> socket, MoveAction move)
        {
            if (!IsSocketOnTurn(socket)) return;
            bool success = game.Move(BoardPosition.FromNotation(move.sourcePosition), BoardPosition.FromNotation(move.targetPosition));
            if (success)
            {
                socket.server.Broadcast(move);
                game.Turn();
                socket.server.Broadcast(new TurnAction(game.currentColor));
            }
            else
            {
                socket.Send(new MessageAction(sender, "Move not possible"));
            }
        }

        private void HandleTurnAction(ServerSocket<Action> socket, TurnAction turn)
        {
            if (!IsSocketOnTurn(socket)) return;
            game.Turn();
            socket.server.Broadcast(new TurnAction(game.currentColor));
        }

        private void HandleParticipationAction(ServerSocket<Action> socket, ParticipationAction participation)
        {
            switch (participation.participation)
            {
                case ParticipationType.Join:
                    HandleParticipationJoin(socket, participation);
                    break;
                case ParticipationType.Leave:
                    HandleParticipationLeave(socket, participation);
                    break;
                case ParticipationType.View:
                    HandleParticipationView(socket, participation);
                    break;
            }
        }

        private void HandleParticipationJoin(ServerSocket<Action> socket, ParticipationAction participation)
        {
            if (players.GetCount() >= 2)
            {
                socket.Send(new MessageAction(sender, "Game already full"));
                return;
            }
            PieceColor color = participation.color ?? PieceColor.White;
            if (players.GetCount() == 1)
            {
                color = players.GetPlayer(PieceColor.White) is null ? PieceColor.White : PieceColor.Black;
            }
            bool success = players.AddPlayer(new ServerPlayer(socket, color));
            if (success)
            {
                socket.server.Broadcast(new MessageAction(sender, $"{socket} joined as {color}"));
                socket.Send(new ParticipationAction(color));
            }
            else
            {
                // TODO: Handle
                socket.Send(new MessageAction(sender, "Something went wrong when trying to join"));
            }
        }

        private void HandleParticipationLeave(ServerSocket<Action> socket, ParticipationAction participation)
        {
            ServerPlayer? player = players.GetPlayer(socket);
            socket.Send(new ParticipationAction(ParticipationType.Leave));
            if (player is null) return;
            players.RemovePlayer(player);
            socket.server.Broadcast(new MessageAction(sender, $"{socket} left the game"));
        }

        private void HandleParticipationView(ServerSocket<Action> socket, ParticipationAction participation)
        {
            socket.server.Broadcast(new MessageAction(sender, $"{socket} joined as viewer"));
        }
    }
}
