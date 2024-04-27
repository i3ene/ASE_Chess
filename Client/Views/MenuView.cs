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
            title.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            title.position.y = new ComponentValue(ComponentUnit.Auto);
            title.textAlignment.horizontal = ComponentHorizontalAlignment.Center;

            int height = 0;
            Settings settings = Settings.getInstance();
            if (settings.GetWidth() >= 80 && settings.GetHeight() >= 28)
            {
                title.SetText(
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
                title.SetText(
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
                title.SetText("ASE Chess");
                height = 1;
            }
            title.size.height = new ComponentValue(ComponentUnit.Fixed, height);

            ContentString text = new ContentString();
            text.Add("Press ");
            text.Add(new ContentString("[↑]").Background(ContentColor.PURPLE));
            text.Add(" or ");
            text.Add(new ContentString("[↓]").Background(ContentColor.PURPLE));
            text.Add(" to change selection.");
            text.Add("\nPress ");
            text.Add(new ContentString("[Enter]").Background(ContentColor.PURPLE));
            text.Add(" to confirm.");

            TextComponent info = new TextComponent();
            info.SetText(text);
            info.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            info.size.height = new ComponentValue(ComponentUnit.Auto);
            info.position.y = new ComponentValue(ComponentUnit.Auto);
            info.alignment.vertical = ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontal = ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            list.size.height = new ComponentValue(ComponentUnit.Auto);
            list.position.y = new ComponentValue(ComponentUnit.Auto, 1);

            ButtonComponent joinButton = new ButtonComponent("Join");
            joinButton.size.width = new ComponentValue(ComponentUnit.Auto);
            joinButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            joinButton.position.y = new ComponentValue(ComponentUnit.Auto);
            joinButton.alignment.horizontal = ComponentHorizontalAlignment.Center;
            joinButton.OnSelection += (button) => ViewJoin();

            ButtonComponent hostButton = new ButtonComponent("Host");
            hostButton.size.width = new ComponentValue(ComponentUnit.Auto);
            hostButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            hostButton.position.y = new ComponentValue(ComponentUnit.Auto);
            hostButton.alignment.horizontal = ComponentHorizontalAlignment.Center;
            hostButton.OnSelection += (button) => ViewHost();

            ButtonComponent viewButton = new ButtonComponent("View");
            viewButton.size.width = new ComponentValue(ComponentUnit.Auto);
            viewButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            viewButton.position.y = new ComponentValue(ComponentUnit.Auto);
            viewButton.alignment.horizontal = ComponentHorizontalAlignment.Center;
            viewButton.OnSelection += (button) => ViewView();

            ButtonComponent settingsButton = new ButtonComponent("Settings");
            settingsButton.size.width = new ComponentValue(ComponentUnit.Auto);
            settingsButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            settingsButton.position.y = new ComponentValue(ComponentUnit.Auto);
            settingsButton.alignment.horizontal = ComponentHorizontalAlignment.Center;
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
