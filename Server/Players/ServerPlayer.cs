using Logic.Pieces;
using Server.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
