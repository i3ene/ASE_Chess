using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Games;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.Communication
{
    public class ClientGameInteraction : IGameInteraction
    {
        private PieceColor? color;
        private readonly ClientSocket<Logic.Communications.Actions.Action> socket;
        public event EventHandler<GameInteractionArguments<TurnAction>>? Turn;
        public event EventHandler<GameInteractionArguments<MoveAction>>? Move;
        public event EventHandler<GameInteractionArguments<SynchronisationAction>>? Synchronisation;

        public ClientGameInteraction(PieceColor? color, ClientSocket<Logic.Communications.Actions.Action> socket)
        {
            this.color = color;
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
                case ActionType.Turn:
                    Turn?.Invoke(this, new GameInteractionArguments<TurnAction>((TurnAction) data));
                    break;
                case ActionType.Move:
                    Move?.Invoke(this, new GameInteractionArguments<MoveAction>((MoveAction) data));
                    break;
                case ActionType.Synchronisation:
                    SynchronisationAction sync = (SynchronisationAction) data;
                    color = sync.role;
                    Synchronisation?.Invoke(this, new GameInteractionArguments<SynchronisationAction>(sync));
                    break;
            }
        }

        public void DoTurn()
        {
            if (color is null) return;
            Turn?.Invoke(this, new GameInteractionArguments<TurnAction>(color, new TurnAction()));
        }

        public void DoMove(BoardPosition source, BoardPosition target)
        {
            if (color is null) return;
            Move?.Invoke(this, new GameInteractionArguments<MoveAction>(color, new MoveAction(source.GetNotation(), target.GetNotation())));
        }
    }
}
