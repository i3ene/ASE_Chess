using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Interactions
{
    public class InteractionArgument
    {
        public readonly InteractionService sender;
        public bool handled;
        public ConsoleKeyInfo key;

        public InteractionArgument(InteractionService sender, ConsoleKeyInfo key)
        {
            handled = false;
            this.sender = sender;
            this.key = key;
        }
    }
}
