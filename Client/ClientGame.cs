using Client.Communications;
using Logic;
using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Pieces;
using Action = Logic.Communications.Actions.Action;

namespace Client
{
    public class ClientGame : Game
    {
        public delegate void GameChangeHandler();
        public GameChangeHandler? OnChange;

        private PieceColor? color;
        private readonly ClientSocket<Action> socket;

        public ClientGame(ClientSocket<Action> socket) : base()
        {
            this.socket = socket;

            this.socket.Connected += SocketConnected;
            this.socket.Data += SocketData;
        }

        public PieceColor? GetOwnColor()
        {
            return color;
        }

        private void SocketConnected()
        {
            socket.Send(new SynchronisationAction());
        }

        private void SocketData(Action data)
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
            OnChange?.Invoke();
        }

        private void HandleTurn(TurnAction turn)
        {
            base.Turn();
            OnChange?.Invoke();
        }

        private void HandleMove(MoveAction move)
        {
            base.Move(BoardPosition.FromNotation(move.sourcePosition), BoardPosition.FromNotation(move.targetPosition));
            OnChange?.Invoke();
        }

        private void HandleSynchronisation(SynchronisationAction sync)
        {
            color = sync.role;
            board.RemoveAllPieces();
            board.AddPieces(sync.pieces);
            currentColor = sync.turn;
            OnChange?.Invoke();
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
