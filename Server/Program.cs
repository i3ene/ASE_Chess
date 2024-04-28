namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Uri uri = new Uri("http://localhost:8080/");
            Server server = new Server(uri);
            Task serverLoop = server.Start();
            Console.Write($"Server listening on {uri.Authority} ...");
            serverLoop.Wait();
        }
    }
}