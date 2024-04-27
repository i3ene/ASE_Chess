using Client.Communications;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
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
            ChatView chatView = new ChatView(router, chat);
            chatView.size.width = new StyleValue(StyleUnit.Relative, 50);
            chatView.position.x = new StyleValue(StyleUnit.Relative, 50);
            chatView.border.positions = [BorderPosition.Left];
            chatView.border.style = BorderStyle.Thin;
            AddChild(chatView);
        }

    }
}
