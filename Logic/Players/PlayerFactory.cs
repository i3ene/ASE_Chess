using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Players
{
    public class PlayerFactory
    {
        private readonly PieceService pieceService;

        public PlayerFactory(PieceService pieceService)
        {
            this.pieceService = pieceService;
        }

        public List<Player> CreatePlayers()
        {
            List<Player> players = new List<Player>();
            foreach (PieceColor color in  pieceService.GetPieceColors())
            {
                players.Add(new Player(color));
            }
            return players;
        }
    }
}
