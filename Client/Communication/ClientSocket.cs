using Logic.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication
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
