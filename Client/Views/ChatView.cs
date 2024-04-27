using Client.Communications;
using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Client.Views.Interactions;
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
            info.size.width = new StyleValue(StyleUnit.Relative, 100);
            info.size.height = new StyleValue(StyleUnit.Auto);
            info.position.y = new StyleValue(StyleUnit.Auto);
            info.alignment.vertical = VerticalAlignment.Bottom;
            info.textAlignment.horizontal = HorizontalAlignment.Center;
            info.border.positions = [BorderPosition.Top];
            info.border.style = BorderStyle.Thin;
            AddChild(info);

            ContentString text = new ContentString();
            text += "Type to enter text.";
            text += "\nPress ";
            text += new ContentString("[Tab]").Background(ContentColor.PURPLE);
            text += " to send message.";
            info.SetText(text);

            input = new InputComponent();
            input.size.width = new StyleValue(StyleUnit.Relative, 100);
            input.size.height = new StyleValue(StyleUnit.Auto);
            input.position.y = new StyleValue(StyleUnit.Auto);
            input.alignment.vertical = VerticalAlignment.Bottom;
            input.border.positions = [BorderPosition.Top];
            input.border.style = BorderStyle.Default;
            AddChild(input);

            messagesContainer = new ComponentContainer();
            messagesContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            messagesContainer.size.height = new StyleValue(StyleUnit.Auto);
            messagesContainer.position.y = new StyleValue(StyleUnit.Auto);
            messagesContainer.alignment.vertical = VerticalAlignment.Bottom;
            AddChild(messagesContainer);
        }

        private void UpdateChat()
        {
            messagesContainer.RemoveAllChilds();

            foreach (ChatMessage message in chat.GetAllMessages().Reverse())
            {
                TextComponent messageText = new TextComponent();
                messageText.size.width = new StyleValue(StyleUnit.Auto);
                messageText.size.height = new StyleValue(StyleUnit.Auto);
                messageText.position.y = new StyleValue(StyleUnit.Auto);
                messageText.alignment.vertical = VerticalAlignment.Bottom;
                messageText.border.style = BorderStyle.Round;
                messageText.alignment.horizontal = HorizontalAlignment.Right;

                ContentString text = new ContentString(message.message);
                switch (message.sender)
                {
                    case "":
                        messageText.alignment.horizontal = HorizontalAlignment.Center;
                        messageText.textAlignment.horizontal = HorizontalAlignment.Center;
                        text.Foreground(ContentColor.DARKGRAY);
                        break;
                    case "self":
                        messageText.alignment.horizontal = HorizontalAlignment.Right;
                        messageText.textAlignment.horizontal = HorizontalAlignment.Right;
                        break;
                    default:
                        messageText.alignment.horizontal = HorizontalAlignment.Left;
                        messageText.textAlignment.horizontal = HorizontalAlignment.Left;
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
