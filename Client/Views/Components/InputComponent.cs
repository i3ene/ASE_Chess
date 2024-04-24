using Client.Views.Contents;
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
            if (args.key.Key == ConsoleKey.Enter) return;

            if (args.key.Key == ConsoleKey.Backspace)
            {
                ContentString text = GetText();
                int length = Math.Max(text.Length - 2, -1);
                text = text.Substring(0, length);
                SetText(text);
                args.handled = true;
                return;
            }

            if (char.IsAscii(args.key.KeyChar) && !char.IsControl(args.key.KeyChar))
            {
                ContentString text = GetText();
                text.Add(args.key.KeyChar.ToString());
                SetText(text);
                args.handled = true;
                return;
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
