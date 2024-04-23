using Client.Views.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Interactions
{
    public class InteractionService
    {
        private readonly IContainer container;

        public InteractionService(IContainer container)
        {
            this.container = container;
        }

        public Task ListenForInteraction()
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    InteractionArgument args = new InteractionArgument(key);
                    InvokeInteractionEvent(container, args);
                }
            });
        }

        private void InvokeInteractionEvent(IContainer container, InteractionArgument args)
        {
            foreach (object child in container.GetAllChilds())
            {
                if (args.handled) break;
                if (child is IInteraction)
                {
                    ((IInteraction)child).HandleInteraction(args);
                }
                if (child is IContainer)
                {
                    InvokeInteractionEvent((IContainer)child, args);
                }
            }
        }
    }
}
