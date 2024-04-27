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
            gameView.size.widthUnit = ComponentUnit.Relative;
            gameView.size.width = 50;
            AddChild(gameView);

            ChatRepository chat = new ChatRepository();
            ChatView chatView = new ChatView(router, chat);
            chatView.size.widthUnit = ComponentUnit.Relative;
            chatView.size.width = 50;
            chatView.position.xUnit = ComponentUnit.Relative;
            chatView.position.x = 50;
            chatView.border.positions = [ComponentBorderPosition.Left];
            chatView.border.style = ComponentBorderStyle.Thin;
            AddChild(chatView);
        }

    }
}
