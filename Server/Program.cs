namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Uri uri = new Uri("http://localhost:8080/");
            ServerInstance server = new ServerInstance(uri);
            Task serverLoop = server.Start();
            Console.Write($"Server listening on {uri.Authority} ...");
            serverLoop.Wait();
        }
    }
}