using Client.Communications;
using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Action = Logic.Communications.Actions.Action;

namespace Client.Views
{
    public class ConnectionView : View
    {
        public readonly ConnectionType connectionType;
        private readonly TextComponent info;

        public ConnectionView(ViewRouter router, ConnectionType connectionType) : base(router)
        {
            this.connectionType = connectionType;

            TextComponent title = new TextComponent("Connection");
            title.size.width = new ComponentValue(ComponentUnit.Auto);
            title.size.height = new ComponentValue(ComponentUnit.Fixed, 2);
            title.position.y = new ComponentValue(ComponentUnit.Auto);
            title.alignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            title.border.positions = [ComponentBorderPosition.Bottom];
            title.border.style = ComponentBorderStyle.Thin;

            AddChild(title);

            info = new TextComponent();
            info.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            info.size.height = new ComponentValue(ComponentUnit.Auto);
            info.position.y = new ComponentValue(ComponentUnit.Auto);
            info.alignment.vertical = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            list.size.height = new ComponentValue(ComponentUnit.Auto);
            list.alignment.vertical = Components.Styles.Alignments.ComponentVerticalAlignment.Middle;
            list.OnUpdate += () => UpdateInfoText(list);

            AddChild(list);

            if (connectionType != ConnectionType.Host)
            {
                ComponentContainer ipContainer = new ComponentContainer();
                ipContainer.size.width = new ComponentValue(ComponentUnit.Relative, 100);
                ipContainer.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
                ipContainer.position.y = new ComponentValue(ComponentUnit.Auto);

                TextComponent ipText = new TextComponent("IP: ");
                ipText.size.width = new ComponentValue(ComponentUnit.Auto);
                ipText.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
                ipText.position.x = new ComponentValue(ComponentUnit.Auto);

                InputComponent ipInput = new InputComponent();
                ipInput.size.width = new ComponentValue(ComponentUnit.Auto);
                ipInput.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
                ipInput.position.x = new ComponentValue(ComponentUnit.Auto);

                ipContainer.AddChild(ipText);
                ipContainer.AddChild(ipInput);
                list.AddChild(ipContainer);
            }

            ComponentContainer portContainer = new ComponentContainer();
            portContainer.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            portContainer.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            portContainer.position.y = new ComponentValue(ComponentUnit.Auto);

            TextComponent portText = new TextComponent("Port: ");
            portText.size.width = new ComponentValue(ComponentUnit.Auto);
            portText.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            portText.position.x = new ComponentValue(ComponentUnit.Auto);

            InputComponent portInput = new InputComponent();
            portInput.size.width = new ComponentValue(ComponentUnit.Auto);
            portInput.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            portInput.position.x = new ComponentValue(ComponentUnit.Auto);

            portContainer.AddChild(portText);
            portContainer.AddChild(portInput);
            list.AddChild(portContainer);


            ButtonComponent actionButton = new ButtonComponent();
            actionButton.size.width = new ComponentValue(ComponentUnit.Auto);
            actionButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            actionButton.position.y = new ComponentValue(ComponentUnit.Auto);
            actionButton.alignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
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
            backButton.size.width = new ComponentValue(ComponentUnit.Auto);
            backButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            backButton.position.y = new ComponentValue(ComponentUnit.Auto);
            backButton.alignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            backButton.OnSelection += (button) => ViewMenu();
            list.AddChild(backButton);
        }

        private void ViewGame()
        {
            // TODO
            ClientSocket<Action> socket = new ClientSocket<Action>();
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
            text.Add("Press ");
            text.Add(new ContentString("[↑]").Background(ContentColor.PURPLE));
            text.Add(" or ");
            text.Add(new ContentString("[↓]").Background(ContentColor.PURPLE));
            text.Add(" to change selection.");

            Component selected = list.GetCurrentSelection();
            if (selected is ComponentContainer)
            {
                text.Add("\nType to enter text.");
            }
            else
            {
                text.Add("\nPress ");
                text.Add(new ContentString("[Enter]").Background(ContentColor.PURPLE));
                text.Add(" to confirm.");
            }

            info.SetText(text);
        }
    }
}
