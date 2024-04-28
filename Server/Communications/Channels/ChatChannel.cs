using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Communications.Actions;
using Server.Communications;
using Action = Logic.Communications.Actions.Action;

namespace Server.Communications.Channels
{
    public class ChatChannel : Channel
    {
        public ChatChannel() : base("chat") { }

        public override void RegisterServer(SocketServer<Action> server)
        {
            server.SocketConnected += ServerSocketConnected;
            server.SocketDisconnected += ServerSocketDisconnected;
            server.SocketData += ServerSocketData;
        }

        private void ServerSocketDisconnected(ServerSocket<Action> socket)
        {
            socket.server.Broadcast(new MessageAction(sender, $"{socket} left the chat"));
        }

        private void ServerSocketConnected(ServerSocket<Action> socket)
        {
            socket.Broadcast(new MessageAction(sender, $"{socket} joined the chat"));
            socket.Send(new MessageAction(sender, $"Welcome {socket} to the chat!"));
        }

        private void ServerSocketData(ServerSocket<Action> socket, Action data)
        {
            if (data.type != ActionType.Message) return;
            MessageAction message = new MessageAction(socket.ToString(), ((MessageAction)data).message);
            socket.Broadcast(message);
        }
    }
}
