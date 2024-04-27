using Logic.Communications;
using System.Net.WebSockets;

namespace Client.Communications
{
    public class ClientSocket<T> : Socket<T>
    {
        public readonly CancellationToken token = new CancellationToken();

        public ClientSocket() : base(new ClientWebSocket()) { }

        public async Task ConnectAsync(Uri uri)
        {
            await ((ClientWebSocket)_socket).ConnectAsync(uri, token);
            await Listen();
        }
    }
}
