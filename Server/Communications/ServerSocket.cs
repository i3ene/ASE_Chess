using Logic.Communications;
using System.Net.WebSockets;

namespace Server.Communications
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
