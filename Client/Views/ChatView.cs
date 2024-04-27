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
            info.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            info.size.height = new ComponentValue(ComponentUnit.Auto);
            info.position.y = new ComponentValue(ComponentUnit.Auto);
            info.alignment.vertical = ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontal = ComponentHorizontalAlignment.Center;
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
            input.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            input.size.height = new ComponentValue(ComponentUnit.Auto);
            input.position.y = new ComponentValue(ComponentUnit.Auto);
            input.alignment.vertical = ComponentVerticalAlignment.Bottom;
            input.border.positions = [ComponentBorderPosition.Top];
            input.border.style = ComponentBorderStyle.Default;
            AddChild(input);

            messagesContainer = new ComponentContainer();
            messagesContainer.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            messagesContainer.size.height = new ComponentValue(ComponentUnit.Auto);
            messagesContainer.position.y = new ComponentValue(ComponentUnit.Auto);
            messagesContainer.alignment.vertical = ComponentVerticalAlignment.Bottom;
            AddChild(messagesContainer);
        }

        private void UpdateChat()
        {
            messagesContainer.RemoveAllChilds();

            foreach (ChatMessage message in chat.GetAllMessages().Reverse())
            {
                TextComponent messageText = new TextComponent();
                messageText.size.width = new ComponentValue(ComponentUnit.Auto);
                messageText.size.height = new ComponentValue(ComponentUnit.Auto);
                messageText.position.y = new ComponentValue(ComponentUnit.Auto);
                messageText.alignment.vertical = ComponentVerticalAlignment.Bottom;
                messageText.border.style = ComponentBorderStyle.Round;
                messageText.alignment.horizontal = ComponentHorizontalAlignment.Right;

                ContentString text = new ContentString(message.message);
                switch (message.sender)
                {
                    case "":
                        messageText.alignment.horizontal = ComponentHorizontalAlignment.Center;
                        messageText.textAlignment.horizontal = ComponentHorizontalAlignment.Center;
                        text.Foreground(ContentColor.DARKGRAY);
                        break;
                    case "self":
                        messageText.alignment.horizontal = ComponentHorizontalAlignment.Right;
                        messageText.textAlignment.horizontal = ComponentHorizontalAlignment.Right;
                        break;
                    default:
                        messageText.alignment.horizontal = ComponentHorizontalAlignment.Left;
                        messageText.textAlignment.horizontal = ComponentHorizontalAlignment.Left;
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
