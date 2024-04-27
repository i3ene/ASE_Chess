using Client.Views.Interactions;

namespace Client.Views.Components
{
    public class ButtonComponent : TextComponent, IInteractable
    {
        public delegate void ButtonSelectionHandler(ButtonComponent sender);
        public event ButtonSelectionHandler? OnSelection;

        public ButtonComponent() : base() { }

        public ButtonComponent(string text) : base(text) { }

        public void HandleInteraction(InteractionArgument args)
        {
            if (args.key.Key == ConsoleKey.Enter)
            {
                OnSelection?.Invoke(this);
                args.handled = true;
            }
        }
    }
}
