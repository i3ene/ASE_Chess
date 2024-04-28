using Server.Communications;
using Action = Logic.Communications.Actions.Action;

namespace Server.Communications.Channels
{
    public class ChannelServer
    {
        private readonly SocketServer<Action> server;
        private readonly List<Channel> channels;

        public ChannelServer(SocketServer<Action> server)
        {
            this.server = server;
            channels = new List<Channel>();
        }

        public void AddChannel(Channel channel)
        {
            channel.RegisterServer(server);
            channels.Add(channel);
        }

        public Channel[] GetChannels(string name)
        {
            return channels.FindAll(channel => channel.sender == name).ToArray();
        }

        public bool RemoveChannel(Channel channel)
        {
            return channels.Remove(channel);
        }

        public void RemoveChannels(string name)
        {
            Channel[] channels = GetChannels(name);
            foreach (Channel channel in channels)
            {
                RemoveChannel(channel);
            }
        }
    }
}
