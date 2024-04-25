using Client.Communications;
using Client.Views.Components;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Client.Views.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class ChatView : View, IInteraction
    {
        private readonly ChatRepository chat;
        private readonly InputComponent input;
        private readonly ComponentContainer messagesContainer;

        public ChatView(ViewRouter router, ChatRepository chat) : base(router)
        {
            this.chat = chat;
            this.chat.OnChange += UpdateChat;

            input = new InputComponent();
            input.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            input.size.width = 100;
            input.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            input.position.yUnit = Components.Styles.ComponentUnit.Auto;
            input.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            input.border.positions = [ComponentBorderPosition.Top];
            input.border.style = ComponentBorderStyle.Thin;
            AddChild(input);

            messagesContainer = new ComponentContainer();
            messagesContainer.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            messagesContainer.size.width = 100;
            messagesContainer.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            messagesContainer.position.yUnit = Components.Styles.ComponentUnit.Auto;
            messagesContainer.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            AddChild(messagesContainer);
        }

        private void UpdateChat()
        {
            messagesContainer.RemoveAllChilds();

            foreach (ChatMessage message in chat.GetAllMessages())
            {
                TextComponent messageText = new TextComponent();
                messageText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
                messageText.size.heightUnit = Components.Styles.ComponentUnit.Auto;
                messageText.position.yUnit = Components.Styles.ComponentUnit.Auto;
                messageText.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;

                ContentString text = new ContentString(message.message);
                if (message.sender == string.Empty)
                {
                    messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Right;
                    text.Foreground(ContentColor.DARKGRAY);
                } 
                else
                {
                    messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Left;
                    text.Insert(0, new ContentString($"\n{message.sender}:").Foreground(ContentColor.BROWN));
                }

                messageText.SetText(text);
                messagesContainer.AddChild(messageText);
            }
        }

        public void HandleInteraction(InteractionArgument args)
        {
            args.sender.InvokeInteractionEvent(input, args);

            if (args.key.Key == ConsoleKey.Tab)
            {
                chat.AddMessage(new ChatMessage(input.GetText().ToString()));
                input.SetText("");
                args.handled = true;
            }
        }
    }
}
