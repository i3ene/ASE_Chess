using Client.Views.Components;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;

namespace Client.Views
{
    public class MenuView : View
    {
        public MenuView(ViewRouter router) : base(router)
        {
            TextComponent title = new TextComponent();
            title.size.width = new StyleValue(StyleUnit.Relative, 100);
            title.position.y = new StyleValue(StyleUnit.Auto);
            title.textAlignment.horizontal = HorizontalAlignment.Center;

            int height = 0;
            ContentString titleText = new ContentString();
            Settings settings = Settings.getInstance();
            if (settings.GetWidth() >= 80 && settings.GetHeight() >= 28)
            {
                titleText.Add(
                    "                                              ▄▄                              \n" +
                    "      ██      ▄█▀▀▀█▄████▀▀▀███      ▄▄█▀▀▀█▄███                              \n" +
                    "     ▄██▄    ▄██    ▀█ ██    ▀█    ▄██▀     ▀███                              \n" +
                    "    ▄█▀██▄   ▀███▄     ██   █      ██▀       ▀███████▄   ▄▄█▀██ ▄██▀███▄██▀███\n" +
                    "   ▄█  ▀██     ▀█████▄ ██████      ██         ██    ██  ▄█▀   ████   ▀▀██   ▀▀\n" +
                    "   ████████  ▄     ▀██ ██   █  ▄   ██▄        ██    ██  ██▀▀▀▀▀▀▀█████▄▀█████▄\n" +
                    "  █▀      ██ ██     ██ ██     ▄█   ▀██▄     ▄▀██    ██  ██▄    ▄█▄   ███▄   ██\n" +
                    "▄███▄   ▄████▄▀█████▀▄██████████     ▀▀█████▀████  ████▄ ▀█████▀██████▀██████▀"
                );
                height = 8;
            }
            else if (settings.GetWidth() >= 55)
            {
                titleText.Add(
                    "           _____ ______    _____ _                   \n" +
                    "    /\\    / ____|  ____|  / ____| |                  \n" +
                    "   /  \\  | (___ | |__    | |    | |__   ___  ___ ___ \n" +
                    "  / /\\ \\  \\___ \\|  __|   | |    | '_ \\ / _ \\/ __/ __|\n" +
                    " / ____ \\ ____) | |____  | |____| | | |  __/\\__ \\__ \\\n" +
                    "/_/    \\_\\_____/|______|  \\_____|_| |_|\\___||___/___/"
                );
                height = 6;
            }
            else
            {
                titleText.Add("ASE Chess");
                height = 1;
            }
            title.SetText(titleText.Foreground(ContentColor.PURPLE));
            title.size.height = new StyleValue(StyleUnit.Fixed, height);

            ContentString text = new ContentString();
            text += "Press ";
            text += new ContentString("[↑]").Background(ContentColor.PURPLE);
            text += " or ";
            text += new ContentString("[↓]").Background(ContentColor.PURPLE);
            text += " to change selection.";
            text += "\nPress ";
            text += new ContentString("[Enter]").Background(ContentColor.PURPLE);
            text += " to confirm.";

            TextComponent info = new TextComponent();
            info.SetText(text);
            info.size.width = new StyleValue(StyleUnit.Relative, 100);
            info.size.height = new StyleValue(StyleUnit.Auto);
            info.position.y = new StyleValue(StyleUnit.Auto);
            info.alignment.vertical = VerticalAlignment.Bottom;
            info.textAlignment.horizontal = HorizontalAlignment.Center;
            info.border.positions = [BorderPosition.Top];
            info.border.style = BorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.width = new StyleValue(StyleUnit.Relative, 100);
            list.size.height = new StyleValue(StyleUnit.Auto);
            list.position.y = new StyleValue(StyleUnit.Auto, 1);

            ButtonComponent joinButton = new ButtonComponent("Join");
            joinButton.size.width = new StyleValue(StyleUnit.Auto);
            joinButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            joinButton.position.y = new StyleValue(StyleUnit.Auto);
            joinButton.alignment.horizontal = HorizontalAlignment.Center;
            joinButton.OnSelection += (button) => ViewJoin();

            ButtonComponent hostButton = new ButtonComponent("Host");
            hostButton.size.width = new StyleValue(StyleUnit.Auto);
            hostButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            hostButton.position.y = new StyleValue(StyleUnit.Auto);
            hostButton.alignment.horizontal = HorizontalAlignment.Center;
            hostButton.OnSelection += (button) => ViewHost();

            ButtonComponent viewButton = new ButtonComponent("View");
            viewButton.size.width = new StyleValue(StyleUnit.Auto);
            viewButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            viewButton.position.y = new StyleValue(StyleUnit.Auto);
            viewButton.alignment.horizontal = HorizontalAlignment.Center;
            viewButton.OnSelection += (button) => ViewView();

            ButtonComponent settingsButton = new ButtonComponent("Settings");
            settingsButton.size.width = new StyleValue(StyleUnit.Auto);
            settingsButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            settingsButton.position.y = new StyleValue(StyleUnit.Auto);
            settingsButton.alignment.horizontal = HorizontalAlignment.Center;
            settingsButton.OnSelection += (button) => ViewSettings();

            list.AddChild(hostButton);
            list.AddChild(joinButton);
            list.AddChild(viewButton);
            list.AddChild(settingsButton);

            AddChild(title);
            AddChild(list);
        }

        private void ViewJoin()
        {
            ConnectionView connection = new ConnectionView(router, ConnectionType.Join);
            router.Display(connection);
        }

        private void ViewHost()
        {
            ConnectionView connection = new ConnectionView(router, ConnectionType.Host);
            router.Display(connection);
        }

        private void ViewView()
        {
            ConnectionView connection = new ConnectionView(router, ConnectionType.View);
            router.Display(connection);
        }

        private void ViewSettings()
        {
            SettingsView settings = new SettingsView(router);
            router.Display(settings);
        }
    }
}
