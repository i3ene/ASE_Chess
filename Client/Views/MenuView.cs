using Client.Views.Components;
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

            ButtonComponent hostButton = new ButtonComponent("Host");
            hostButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            hostButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            hostButton.size.height = 3;
            hostButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            hostButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;

            ButtonComponent viewButton = new ButtonComponent("View");
            viewButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            viewButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            viewButton.size.height = 3;
            viewButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            viewButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;

            ButtonComponent settingsButton = new ButtonComponent("Settings");
            settingsButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            settingsButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            settingsButton.size.height = 3;
            settingsButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            settingsButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;

            list.AddChild(joinButton);
            list.AddChild(hostButton);
            list.AddChild(viewButton);
            list.AddChild(settingsButton);
            AddChild(title);
            AddChild(list);
        }

        private void ViewJoin()
        {

        }

        private void ViewHost()
        {

        }

        private void ViewView()
        {

        }

        private void ViewSettings()
        {

        }
    }
}
