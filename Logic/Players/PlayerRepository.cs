using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Players
{
    public class PlayerRepository
    {
        private readonly List<Player> players;

        public PlayerRepository()
        {
            players = new List<Player>();
        }

        public bool AddPlayer(Player player)
        {
            Player? existing = GetPlayer(player.color);
            if (existing != null) return false;


            players.Add(player);
            return true;
        }

        public bool AddPlayers(Player[] players)
        {
            foreach (Player player in players)
            {
                Player? existing = GetPlayer(player.color);
                if (existing != null) return false;
            }

            this.players.AddRange(players);
            return true;
        }

        public bool RemovePlayer(Player player)
        {
            return players.Remove(player);
        }

        public bool RemovePlayer(PieceColor color)
        {
            Player? player = GetPlayer(color);
            if (player == null) return false;
            return RemovePlayer(player);
        }

        public Player? GetPlayer(PieceColor color)
        {
            return players.First(p => p.color == color);
        }
        
    }
}
