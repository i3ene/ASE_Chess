using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Interactions
{
    public class InteractionArgument
    {
        public bool handled;
        public ConsoleKeyInfo key;

        public InteractionArgument(ConsoleKeyInfo key)
        {
            handled = false;
            this.key = key;
        }
    }
}
