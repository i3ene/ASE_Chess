using Logic.Pieces;
using Server.Communications;

namespace Server.Players
{
    public class ServerPlayer
    {
        public ServerSocket<Logic.Communications.Actions.Action> socket;
        public PieceColor color;

        public ServerPlayer(ServerSocket<Logic.Communications.Actions.Action> socket, PieceColor color)
        {
            this.socket = socket;
            this.color = color;
        }
    }
}
