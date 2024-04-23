﻿using Client.Views.Contents;
using Client.Views.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class InputComponent : TextComponent, IInteraction
    {
        public InputComponent() : base() { }

        public void HandleInteraction(InteractionArgument args)
        {
            if (char.IsLetterOrDigit(args.key.KeyChar) || char.IsWhiteSpace(args.key.KeyChar))
            {
                ContentString text = GetText();
                text.Add(args.key.KeyChar.ToString());
                SetText(text);
                args.handled = true;
            }

            if (args.key.Key == ConsoleKey.Backspace)
            {
                ContentString text = GetText();
                int length = Math.Max(text.Length - 2, -1);
                text = text.Substring(0, length);
                SetText(text);
                args.handled = true;
            }
        }

        private new void SetText(string text)
        {
            base.SetText(text);
        }

        private new void SetText(ContentString text)
        {
            base.SetText(text);
        }
    }
}