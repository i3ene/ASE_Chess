using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Players
{
    public interface IPlayerRepository
    {
        public bool AddPlayer(Player player);

        public bool AddPlayers(Player[] players);

        public bool RemovePlayer(Player player);

        public bool RemovePlayer(PieceColor color);

        public Player? GetPlayer(PieceColor color);

        public IEnumerable<Player> GetAllPlayers();
    }
}
