using Logic.Pieces;
using Server.Communications;
using Action = Logic.Communications.Actions.Action;

namespace Server.Players
{
    public class ServerPlayer
    {
        public ServerSocket<Action> socket;
        public PieceColor color;

        public ServerPlayer(ServerSocket<Action> socket, PieceColor color)
        {
            this.socket = socket;
            this.color = color;
        }
    }
}
