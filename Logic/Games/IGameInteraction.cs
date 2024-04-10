using Logic.Boards;
using Logic.Communications.Actions;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Games
{
    public interface IGameInteraction
    {
        public event EventHandler<GameInteractionArguments<TurnAction>>? Turn;
        public event EventHandler<GameInteractionArguments<MoveAction>>? Move;
        public event EventHandler<GameInteractionArguments<SynchronisationAction>>? Synchronisation;
    }
}
