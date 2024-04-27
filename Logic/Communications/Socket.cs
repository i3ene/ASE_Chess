using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace Logic.Communications
{
    public class Socket<T> : WebSocket, IDisposable
    {
        private readonly int BufferSize = 4096;

        protected readonly WebSocket _socket;

        public override WebSocketState State => _socket.State;

        public override WebSocketCloseStatus? CloseStatus => _socket.CloseStatus;
        public override string? CloseStatusDescription => _socket.CloseStatusDescription;
        public override string? SubProtocol => _socket.SubProtocol;

        public delegate void ConnectedHandler();
        public event ConnectedHandler? Connected;

        public delegate void DisconnectedHandler();
        public event DisconnectedHandler? Disconnected;

        public delegate void DataHandler(T data);
        public event DataHandler? Data;

        public delegate void ErrorHandler(Exception exception);
        public event ErrorHandler? Error;

        public Socket(WebSocket socket)
        {
            _socket = socket;
        }

        public async Task Listen()
        {
            if (State == WebSocketState.Open) Connected?.Invoke();
            while (State == WebSocketState.Open)
            {
                await ListenOnce();
            }
            Disconnected?.Invoke();
        }

        public async Task ListenOnce()
        {
            var buffer = new ArraySegment<byte>(new byte[BufferSize]);
            WebSocketReceiveResult? result;
            var allBytes = new List<byte>();

            do
            {
                result = await ReceiveAsync(buffer, CancellationToken.None);
                for (int i = 0; i < result.Count; i++)
                {
                    allBytes.Add(buffer.Array[i]);
                }
            }
            while (!result.EndOfMessage);

            var text = Encoding.UTF8.GetString(allBytes.ToArray(), 0, allBytes.Count);
            try
            {
                var payload = JsonConvert.DeserializeObject<T>(text, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    NullValueHandling = NullValueHandling.Ignore
                });
                if (payload != null) Data?.Invoke(payload);
            }
            catch (Exception ex)
            {
                Error?.Invoke(ex);
            }
        }

        public void Send(T payload)
        {
            if (State != WebSocketState.Open) return;
            string message = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override void Abort()
        {
            _socket.Abort();
        }

        public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
        {
            return _socket.CloseAsync(closeStatus, statusDescription, cancellationToken);
        }

        public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
        {
            return _socket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
        }

        public override void Dispose()
        {
            _socket.Dispose();
        }

        public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            return _socket.ReceiveAsync(buffer, cancellationToken);
        }

        public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            return _socket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
        }
    }
}
