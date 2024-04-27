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
            title.size.widthUnit = ComponentUnit.Relative;
            title.size.width = 100;
            title.position.yUnit = ComponentUnit.Auto;
            title.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Center;

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
                title.size.height = 8;
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
                title.size.height = 6;
            }
            else
            {
                title.SetText("ASE Chess");
                title.size.height = 1;
            }

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
            info.size.widthUnit = ComponentUnit.Relative;
            info.size.width = 100;
            info.size.heightUnit = ComponentUnit.Auto;
            info.position.yUnit = ComponentUnit.Auto;
            info.alignment.verticalAlignment = ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.widthUnit = ComponentUnit.Relative;
            list.size.width = 100;
            list.size.heightUnit = ComponentUnit.Auto;
            list.position.yUnit = ComponentUnit.Auto;
            list.position.y = 1;

            ButtonComponent joinButton = new ButtonComponent("Join");
            joinButton.size.widthUnit = ComponentUnit.Auto;
            joinButton.size.heightUnit = ComponentUnit.Fixed;
            joinButton.size.height = 3;
            joinButton.position.yUnit = ComponentUnit.Auto;
            joinButton.alignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
            joinButton.OnSelection += (button) => ViewJoin();

            ButtonComponent hostButton = new ButtonComponent("Host");
            hostButton.size.widthUnit = ComponentUnit.Auto;
            hostButton.size.heightUnit = ComponentUnit.Fixed;
            hostButton.size.height = 3;
            hostButton.position.yUnit = ComponentUnit.Auto;
            hostButton.alignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
            hostButton.OnSelection += (button) => ViewHost();

            ButtonComponent viewButton = new ButtonComponent("View");
            viewButton.size.widthUnit = ComponentUnit.Auto;
            viewButton.size.heightUnit = ComponentUnit.Fixed;
            viewButton.size.height = 3;
            viewButton.position.yUnit = ComponentUnit.Auto;
            viewButton.alignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
            viewButton.OnSelection += (button) => ViewView();

            ButtonComponent settingsButton = new ButtonComponent("Settings");
            settingsButton.size.widthUnit = ComponentUnit.Auto;
            settingsButton.size.heightUnit = ComponentUnit.Fixed;
            settingsButton.size.height = 3;
            settingsButton.position.yUnit = ComponentUnit.Auto;
            settingsButton.alignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
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
