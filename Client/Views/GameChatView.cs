using Client.Communications;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Logic.Communications.Actions;
using Action = Logic.Communications.Actions.Action;

namespace Client.Views
{
    public class GameChatView : View
    {
        public GameChatView(ViewRouter router, ClientSocket<Action> socket) : base(router)
        {
            ClientGame game = new ClientGame(socket);
            GameView gameView = new GameView(router, game);
            gameView.size.width = new StyleValue(StyleUnit.Relative, 50);
            AddChild(gameView);

            ChatRepository chat = new ChatRepository();
            chat.OnChange += (message) => ChatOnChange(socket, message);
            socket.Data += (data) => SocketData(data, chat);
            ChatView chatView = new ChatView(router, chat);
            chatView.size.width = new StyleValue(StyleUnit.Relative, 50);
            chatView.position.x = new StyleValue(StyleUnit.Relative, 50);
            chatView.border.positions = [BorderPosition.Left];
            chatView.border.style = BorderStyle.Thin;
            AddChild(chatView);
        }

        private void ChatOnChange(ClientSocket<Action> socket, ChatMessage message)
        {
            if (message.sender != "self") return;
            MessageAction action = new MessageAction(message.message);
            socket.Send(action);
        }

        private void SocketData(Action data, ChatRepository chat)
        {
            if (data.type != ActionType.Message) return;
            MessageAction message = (MessageAction)data;
            ChatMessage chatMessage = new ChatMessage(message.sender, message.message);
            chat.AddMessage(chatMessage);
        }
    }
}
