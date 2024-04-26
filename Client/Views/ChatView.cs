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
using Client.Views.Components.Styles.Alignments;

namespace Client.Views
{
    public class ChatView : View, IInteractable
    {
        private readonly ChatRepository chat;
        private readonly InputComponent input;
        private readonly ComponentContainer messagesContainer;
        private readonly TextComponent info;

        public ChatView(ViewRouter router, ChatRepository chat) : base(router)
        {
            this.chat = chat;
            this.chat.OnChange += UpdateChat;

            info = new TextComponent();
            info.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            info.size.width = 100;
            info.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            info.position.yUnit = Components.Styles.ComponentUnit.Auto;
            info.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            ContentString text = new ContentString();
            text.Add("Type to enter text.");
            text.Add("\nPress ");
            text.Add(new ContentString("[Tab]").Background(ContentColor.PURPLE));
            text.Add(" to send message.");
            info.SetText(text);

            input = new InputComponent();
            input.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            input.size.width = 100;
            input.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            input.position.yUnit = Components.Styles.ComponentUnit.Auto;
            input.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            input.border.positions = [ComponentBorderPosition.Top];
            input.border.style = ComponentBorderStyle.Default;
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

            foreach (ChatMessage message in chat.GetAllMessages().Reverse())
            {
                TextComponent messageText = new TextComponent();
                messageText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
                messageText.size.heightUnit = Components.Styles.ComponentUnit.Auto;
                messageText.position.yUnit = Components.Styles.ComponentUnit.Auto;
                messageText.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
                messageText.border.style = ComponentBorderStyle.Round;
                messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Right;

                ContentString text = new ContentString(message.message);
                switch (message.sender)
                {
                    case "":
                        messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
                        messageText.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
                        text.Foreground(ContentColor.DARKGRAY);
                        break;
                    case "self":
                        messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Right;
                        messageText.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Right;
                        break;
                    default:
                        messageText.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Left;
                        messageText.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Left;
                        text.Insert(0, new ContentString($"{message.sender}:\n").Foreground(ContentColor.BROWN));
                        break;
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
                chat.AddMessage(new ChatMessage("self", input.GetText().ToString(false)));
                input.SetText("");
                args.handled = true;
            }
        }
    }
}
