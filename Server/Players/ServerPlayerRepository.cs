using Logic.Pieces;
using Server.Communications;

namespace Server.Players
{
    public class ServerPlayerRepository
    {
        public List<ServerPlayer> players;

        public ServerPlayerRepository()
        {
            players = new List<ServerPlayer>();
        }

        public bool AddPlayer(ServerPlayer player)
        {
            ServerPlayer? exists = GetPlayer(player.color);
            if (exists != null) return false;
            exists = players.First(p => p.socket == player.socket);
            RemovePlayer(exists);
            players.Add(player);
            return true;
        }

        public bool RemovePlayer(ServerPlayer player)
        {
            return players.Remove(player);
        }

        public ServerPlayer? GetPlayer(ServerSocket<Logic.Communications.Actions.Action> socket)
        {
            return players.First(p => p.socket == socket);
        }

        public ServerPlayer? GetPlayer(PieceColor color)
        {
            return players.First(p => p.color == color); ;
        }

        public IEnumerable<ServerPlayer> GetPlayers()
        {
            return players.ToList();
        }

        public int GetCount()
        {
            return players.Count;
        }
    }
}
