using Logic.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Communication
{
    public class ServerSocket<T> : Socket<T>
    {
        public readonly SocketServer<T> server;

        public ServerSocket(SocketServer<T> server, WebSocket socket) : base(socket)
        {
            this.server = server;
        }

        public void Broadcast(T message)
        {
            foreach (var socket in server.sockets)
            {
                if (socket != this) socket.Send(message);
            }
        }

        public override string ToString()
        {
            return GetHashCode().ToString();
        }
    }
}
