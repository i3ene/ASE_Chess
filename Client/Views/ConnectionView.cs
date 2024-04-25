using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views.Components;
using Client.Views.Components.Styles.Borders;

namespace Client.Views
{
    public class ConnectionView : View
    {
        public readonly ConnectionType connectionType;

        public ConnectionView(ViewRouter router, ConnectionType connectionType) : base(router)
        {
            this.connectionType = connectionType;

            TextComponent title = new TextComponent("Connection");
            title.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            title.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            title.size.height = 2;
            title.position.yUnit = Components.Styles.ComponentUnit.Auto;
            title.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            title.border.positions = [ComponentBorderPosition.Bottom];
            title.border.style = ComponentBorderStyle.Thin;

            AddChild(title);

            ListComponent list = new ListComponent();
            list.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            list.size.width = 100;
            list.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            list.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Middle;

            AddChild(list);

            if (connectionType != ConnectionType.Host)
            {
                ComponentContainer ipContainer = new ComponentContainer();
                ipContainer.size.widthUnit = Components.Styles.ComponentUnit.Relative;
                ipContainer.size.width = 100;
                ipContainer.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
                ipContainer.size.height = 3;
                ipContainer.position.yUnit = Components.Styles.ComponentUnit.Auto;

                TextComponent ipText = new TextComponent("IP:");
                ipText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
                ipText.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
                ipText.size.height = 1;
                ipText.position.xUnit = Components.Styles.ComponentUnit.Auto;

                InputComponent ipInput = new InputComponent();
                ipInput.size.widthUnit = Components.Styles.ComponentUnit.Auto;
                ipInput.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
                ipInput.size.height = 1;
                ipInput.position.xUnit = Components.Styles.ComponentUnit.Auto;

                ipContainer.AddChild(ipText);
                ipContainer.AddChild(ipInput);
                list.AddChild(ipContainer);
            }

            ComponentContainer portContainer = new ComponentContainer();
            portContainer.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            portContainer.size.width = 100;
            portContainer.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            portContainer.size.height = 3;
            portContainer.position.yUnit = Components.Styles.ComponentUnit.Auto;

            TextComponent portText = new TextComponent("Port:");
            portText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            portText.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            portText.size.height = 1;
            portText.position.xUnit = Components.Styles.ComponentUnit.Auto;

            InputComponent portInput = new InputComponent();
            portInput.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            portInput.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            portInput.size.height = 1;
            portInput.position.xUnit = Components.Styles.ComponentUnit.Auto;

            portContainer.AddChild(portText);
            portContainer.AddChild(portInput);
            list.AddChild(portContainer);


            ButtonComponent actionButton = new ButtonComponent();
            actionButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            actionButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            actionButton.size.height = 3;
            actionButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            actionButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
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
            backButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            backButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            backButton.size.height = 3;
            backButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            backButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            backButton.OnSelection += (button) => ViewMenu();
            list.AddChild(backButton);
        }

        private void ViewGame()
        {
            // TODO
        }

        private void ViewMenu()
        {
            MenuView menu = new MenuView(router);
            router.Display(menu);
        }
    }
}
