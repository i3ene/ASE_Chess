using Client.Views.Components;
using Client.Views.Components.Styles.Borders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class SettingsView : View
    {
        public SettingsView(ViewRouter router) : base(router)
        {
            TextComponent title = new TextComponent("Settings");
            title.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            title.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            title.size.height = 2;
            title.position.yUnit = Components.Styles.ComponentUnit.Auto;
            title.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            title.border.positions = [ComponentBorderPosition.Bottom];
            title.border.style = ComponentBorderStyle.Thin;

            ListComponent list = new ListComponent();
            list.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            list.size.width = 100;
            list.size.heightUnit = Components.Styles.ComponentUnit.Auto;
            list.alignment.verticalAlignment = Components.Styles.Alignments.ComponentVerticalAlignment.Middle;

            ComponentContainer widthContainer = new ComponentContainer();
            widthContainer.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            widthContainer.size.width = 100;
            widthContainer.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            widthContainer.size.height = 3;
            widthContainer.position.yUnit = Components.Styles.ComponentUnit.Auto;

            TextComponent widthText = new TextComponent("Width");
            widthText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            widthText.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            widthText.size.height = 1;
            widthText.position.xUnit = Components.Styles.ComponentUnit.Auto;

            SliderComponent widthSlider = new SliderComponent();
            widthSlider.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            widthSlider.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            widthSlider.size.height = 1;
            widthSlider.position.xUnit = Components.Styles.ComponentUnit.Auto;

            widthContainer.AddChild(widthText);
            widthContainer.AddChild(widthSlider);

            list.AddChild(widthContainer);

            ComponentContainer heightContainer = new ComponentContainer();
            heightContainer.size.widthUnit = Components.Styles.ComponentUnit.Relative;
            heightContainer.size.width = 100;
            heightContainer.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            heightContainer.size.height = 3;
            heightContainer.position.yUnit = Components.Styles.ComponentUnit.Auto;

            TextComponent heightText = new TextComponent("Height");
            heightText.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            heightText.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            heightText.size.height = 1;
            heightText.position.xUnit = Components.Styles.ComponentUnit.Auto;

            SliderComponent heightSlider = new SliderComponent();
            heightSlider.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            heightSlider.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            heightSlider.size.height = 1;
            heightSlider.position.xUnit = Components.Styles.ComponentUnit.Auto;

            heightContainer.AddChild(heightText);
            heightContainer.AddChild(heightSlider);

            list.AddChild(heightContainer);

            ButtonComponent backButton = new ButtonComponent("Back");
            backButton.size.widthUnit = Components.Styles.ComponentUnit.Auto;
            backButton.size.heightUnit = Components.Styles.ComponentUnit.Fixed;
            backButton.size.height = 3;
            backButton.position.yUnit = Components.Styles.ComponentUnit.Auto;
            backButton.alignment.horizontalAlignment = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            backButton.OnSelection += (button) => ViewMenu();

            list.AddChild(backButton);

            AddChild(title);
            AddChild(list);
        }

        public void ViewMenu()
        {
            MenuView menu = new MenuView(router);
            router.Display(menu);
        }
    }
}
