using Client.Communications;
using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Logic.Communications.Actions;
using Action = Logic.Communications.Actions.Action;

namespace Client.Views
{
    public class ConnectionView : View
    {
        public readonly ConnectionType connectionType;
        private readonly TextComponent info;

        private readonly InputComponent ipInput;
        private readonly InputComponent portInput;

        public ConnectionView(ViewRouter router, ConnectionType connectionType) : base(router)
        {
            this.connectionType = connectionType;

            TextComponent title = new TextComponent("Connection");
            title.size.width = new StyleValue(StyleUnit.Auto);
            title.size.height = new StyleValue(StyleUnit.Fixed, 2);
            title.position.y = new StyleValue(StyleUnit.Auto);
            title.alignment.horizontal = Components.Styles.Alignments.HorizontalAlignment.Center;
            title.border.positions = [BorderPosition.Bottom];
            title.border.style = BorderStyle.Thin;

            AddChild(title);

            info = new TextComponent();
            info.size.width = new StyleValue(StyleUnit.Relative, 100);
            info.size.height = new StyleValue(StyleUnit.Auto);
            info.position.y = new StyleValue(StyleUnit.Auto);
            info.alignment.vertical = Components.Styles.Alignments.VerticalAlignment.Bottom;
            info.textAlignment.horizontal = Components.Styles.Alignments.HorizontalAlignment.Center;
            info.border.positions = [BorderPosition.Top];
            info.border.style = BorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.width = new StyleValue(StyleUnit.Relative, 100);
            list.size.height = new StyleValue(StyleUnit.Auto);
            list.alignment.vertical = Components.Styles.Alignments.VerticalAlignment.Middle;
            list.OnUpdate += () => UpdateInfoText(list);

            AddChild(list);

            ComponentContainer ipContainer = new ComponentContainer();
            ipContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            ipContainer.size.height = new StyleValue(StyleUnit.Fixed, 3);
            ipContainer.position.y = new StyleValue(StyleUnit.Auto);

            TextComponent ipText = new TextComponent("IP: ");
            ipText.size.width = new StyleValue(StyleUnit.Auto);
            ipText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            ipText.position.x = new StyleValue(StyleUnit.Auto);

            ipInput = new InputComponent();
            ipInput.size.width = new StyleValue(StyleUnit.Auto);
            ipInput.size.height = new StyleValue(StyleUnit.Fixed, 1);
            ipInput.position.x = new StyleValue(StyleUnit.Auto);

            ipContainer.AddChild(ipText);
            ipContainer.AddChild(ipInput);

            if (connectionType != ConnectionType.Host)
            {
                list.AddChild(ipContainer);
            }

            ComponentContainer portContainer = new ComponentContainer();
            portContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            portContainer.size.height = new StyleValue(StyleUnit.Fixed, 3);
            portContainer.position.y = new StyleValue(StyleUnit.Auto);

            TextComponent portText = new TextComponent("Port: ");
            portText.size.width = new StyleValue(StyleUnit.Auto);
            portText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            portText.position.x = new StyleValue(StyleUnit.Auto);

            portInput = new InputComponent();
            portInput.size.width = new StyleValue(StyleUnit.Auto);
            portInput.size.height = new StyleValue(StyleUnit.Fixed, 1);
            portInput.position.x = new StyleValue(StyleUnit.Auto);

            portContainer.AddChild(portText);
            portContainer.AddChild(portInput);
            list.AddChild(portContainer);


            ButtonComponent actionButton = new ButtonComponent();
            actionButton.size.width = new StyleValue(StyleUnit.Auto);
            actionButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            actionButton.position.y = new StyleValue(StyleUnit.Auto);
            actionButton.alignment.horizontal = Components.Styles.Alignments.HorizontalAlignment.Center;
            actionButton.OnSelection += (button) => ViewGame();
            list.AddChild(actionButton);

            switch (this.connectionType)
            {
                case ConnectionType.Host:
                    actionButton.SetText("Host");
                    break;
                case ConnectionType.Join:
                    actionButton.SetText("Join");
                    break;
                case ConnectionType.View:
                    actionButton.SetText("View");
                    break;
            }

            ButtonComponent backButton = new ButtonComponent("Back");
            backButton.size.width = new StyleValue(StyleUnit.Auto);
            backButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            backButton.position.y = new StyleValue(StyleUnit.Auto);
            backButton.alignment.horizontal = Components.Styles.Alignments.HorizontalAlignment.Center;
            backButton.OnSelection += (button) => ViewMenu();
            list.AddChild(backButton);
        }

        private Uri GetUri() => connectionType switch
        {
            ConnectionType.Host => new Uri($"ws://127.0.0.1:{portInput.GetText().ToString(false)}"),
            ConnectionType.Join or
            ConnectionType.View => new Uri($"ws://{ipInput.GetText().ToString(false)}:{portInput.GetText().ToString(false)}"),
            _ => new Uri("")
        };

        private ParticipationType GetParticipationType() => connectionType switch
        {
            ConnectionType.Host or
            ConnectionType.Join => ParticipationType.Join,
            ConnectionType.View => ParticipationType.View,
            _ => ParticipationType.Leave
        };

        private ClientSocket<Action> CreateConnection()
        {
            Uri uri = GetUri();
            if (connectionType == ConnectionType.Host)
            {
                // TODO: Set up server
            }

            ClientSocket<Action> socket = new ClientSocket<Action>();
            Task connection = socket.ConnectAsync(uri);

            ParticipationType participation = GetParticipationType();
            socket.Send(new ParticipationAction(participation));
            return socket;
        }

        private void ViewGame()
        {
            ClientSocket<Action> socket = CreateConnection();
            GameChatView game = new GameChatView(router, socket);
            router.Display(game);
        }

        private void ViewMenu()
        {
            MenuView menu = new MenuView(router);
            router.Display(menu);
        }

        private void UpdateInfoText(ListComponent list)
        {
            ContentString text = new ContentString();
            text += "Press ";
            text += new ContentString("[↑]").Background(ContentColor.PURPLE);
            text += " or ";
            text += new ContentString("[↓]").Background(ContentColor.PURPLE);
            text += " to change selection.";

            Component selected = list.GetCurrentSelection();
            if (selected is ComponentContainer)
            {
                text += "\nType to enter text.";
            }
            else
            {
                text += "\nPress ";
                text += new ContentString("[Enter]").Background(ContentColor.PURPLE);
                text += " to confirm.";
            }

            info.SetText(text);
        }
    }
}
