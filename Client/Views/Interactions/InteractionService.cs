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
            Console.CursorVisible = false;
        }

        public Task ListenForInteraction()
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    InteractionArgument args = new InteractionArgument(this, key);
                    TraverseContainer(container, args);
                }
            });
        }

        private void TraverseContainer(IContainer container, InteractionArgument args)
        {
            foreach (object child in container.GetAllChilds())
            {
                if (args.handled) break;
                InvokeInteractionEvent(child, args);
            }
        }

        public void InvokeInteractionEvent(object element, InteractionArgument args)
        {
            if (element is IInteraction)
            {
                ((IInteraction)element).HandleInteraction(args);
            }
            if (element is IContainer)
            {
                TraverseContainer((IContainer)element, args);
            }
        }
    }
}
