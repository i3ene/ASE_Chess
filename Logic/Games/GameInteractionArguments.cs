using Logic.Communications.Actions;
using Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Games
{
    public class GameInteractionArguments<T> where T : Communications.Actions.Action
    {
        public PieceColor? actor;
        public T action;
        public bool handled;

        public GameInteractionArguments(T action) : this(null, action) { }

        public GameInteractionArguments(PieceColor? actor, T action)
        {
            this.actor = actor;
            this.action = action;
            handled = false;
        }
    }
}
