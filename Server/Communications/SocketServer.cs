using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.WebSockets;
using System.Net;

namespace Server.Communications
{
    public class SocketServer<T>
    {
        private readonly HttpListener httpListener = new HttpListener();
        private readonly Mutex signal = new Mutex();

        public readonly ObservableCollection<ServerSocket<T>> sockets = new ObservableCollection<ServerSocket<T>>();

        public delegate void SocketConnectedHandler(ServerSocket<T> socket);
        public event SocketConnectedHandler? SocketConnected;

        public delegate void SocketDisconnectedHandler(ServerSocket<T> socket);
        public event SocketDisconnectedHandler? SocketDisconnected;

        public delegate void SocketDataHandler(ServerSocket<T> socket, T data);
        public event SocketDataHandler? SocketData;

        public delegate void SocketErrorHandler(ServerSocket<T> socket, Exception exception);
        public event SocketErrorHandler? SocketError;

        public SocketServer(string uri)
        {
            httpListener.Prefixes.Add(uri);
            sockets.CollectionChanged += SocketsCollectionChanged;
        }

        public void SocketsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (
                e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace
            )
            {
                e.NewItems?.Cast<ServerSocket<T>>().ToList().ForEach(socket => Subscribe(socket));
            }
            if (
                e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace ||
                e.Action == NotifyCollectionChangedAction.Reset
            )
            {
                e.OldItems?.Cast<ServerSocket<T>>().ToList().ForEach(socket => Unsubscribe(socket));
            }
        }

        public Task Start()
        {
            return Task.Factory.StartNew(() =>
            {
                httpListener.Start();
                while (signal.WaitOne())
                {
                    Listen();
                }
            });
        }

        public void Broadcast(T data)
        {
            foreach (var socket in sockets)
            {
                socket.Send(data);
            }
        }

        private async Task Listen()
        {
            HttpListenerContext context = httpListener.GetContext();
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                ServerSocket<T> socket = new ServerSocket<T>(this, webSocketContext.WebSocket);
                sockets.Add(socket);
                await socket.Listen();
                sockets.Remove(socket);
            }
            signal.ReleaseMutex();
        }

        private void Subscribe(ServerSocket<T> socket)
        {
            socket.Data += (data) => SocketData?.Invoke(socket, data);
            socket.Error += (exception) => SocketError?.Invoke(socket, exception);
            socket.Disconnected += () => SocketDisconnected?.Invoke(socket);
            socket.Connected += () => SocketConnected?.Invoke(socket);
        }

        private void Unsubscribe(ServerSocket<T> socket)
        {
            socket.Data -= (data) => SocketData?.Invoke(socket, data);
            socket.Error -= (exception) => SocketError?.Invoke(socket, exception);
            socket.Disconnected -= () => SocketDisconnected?.Invoke(socket);
            socket.Connected -= () => SocketConnected?.Invoke(socket);
        }
    }
}
