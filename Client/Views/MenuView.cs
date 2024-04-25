using Client.Views.Components;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class MenuView : View
    {
        public MenuView(ViewRouter router) : base(router)
        {
            TextComponent title = new TextComponent(
                "           _____ ______    _____ _                   \n" + 
                "    /\\    / ____|  ____|  / ____| |                  \n" + 
                "   /  \\  | (___ | |__    | |    | |__   ___  ___ ___ \n" + 
                "  / /\\ \\  \\___ \\|  __|   | |    | '_ \\ / _ \\/ __/ __|\n" + 
                " / ____ \\ ____) | |____  | |____| | | |  __/\\__ \\__ \\\n" +
                "/_/    \\_\\_____/|______|  \\_____|_| |_|\\___||___/___/"
            );
            title.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            title.size.width = 100;
            title.size.height = 6;
            title.position.yUnit = Components.Styles.ComponentUnit.Auto;
            title.textAlignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;

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
            info.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            info.size.width = 100;
            info.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            info.position.yUnit = Components.Styles.ComponentUnit.Auto;
            info.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            ListComponent list = new ListComponent();
            list.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            list.size.width = 100;
            list.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            list.position.yUnit = Components.Styles.ComponentUnit.Auto;
            list.position.y = 1;

            ButtonComponent joinButton = new ButtonComponent("Join");
            joinButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            joinButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            joinButton.size.height = 3;
            joinButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            joinButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            joinButton.OnSelection += (button) => ViewJoin();

            ButtonComponent hostButton = new ButtonComponent("Host");
            hostButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            hostButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            hostButton.size.height = 3;
            hostButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            hostButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            hostButton.OnSelection += (button) => ViewHost();

            ButtonComponent viewButton = new ButtonComponent("View");
            viewButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            viewButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            viewButton.size.height = 3;
            viewButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            viewButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            viewButton.OnSelection += (button) => ViewView();

            ButtonComponent settingsButton = new ButtonComponent("Settings");
            settingsButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            settingsButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            settingsButton.size.height = 3;
            settingsButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            settingsButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
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
