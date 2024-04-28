using Server.Communications;
using Action = Logic.Communications.Actions.Action;

namespace Server.Communications.Channels
{
    public abstract class Channel
    {
        public readonly string sender;

        public Channel(string sender)
        {
            this.sender = sender;
        }

        public abstract void RegisterServer(SocketServer<Action> server);
    }
}
