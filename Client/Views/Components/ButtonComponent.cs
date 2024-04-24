﻿using Client.Views.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class ButtonComponent : TextComponent, IInteraction
    {
        public delegate void ButtonSelectionHandler(ButtonComponent sender);
        public event ButtonSelectionHandler? OnSelection;

        public ButtonComponent() : base() { }

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