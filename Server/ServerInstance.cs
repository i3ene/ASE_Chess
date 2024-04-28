using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Communications;
using Server.Communications.Channels;
using Action = Logic.Communications.Actions.Action;

namespace Server
{
    public class ServerInstance
    {
        private readonly SocketServer<Action> socket;
        private readonly ChannelServer channel;

        public ServerInstance(Uri uri)
        {
            socket = new SocketServer<Action>(uri.AbsoluteUri);
            channel = new ChannelServer(socket);
            channel.AddChannel(new ChatChannel());
            channel.AddChannel(new GameChannel());
        }

        public Task Start()
        {
            return socket.Start();
        }
    }
}
