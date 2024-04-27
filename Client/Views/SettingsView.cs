using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;

namespace Client.Views
{
    public class SettingsView : View
    {
        private readonly TextComponent info;

        public SettingsView(ViewRouter router) : base(router)
        {
            Settings settings = Settings.getInstance();

            info = new TextComponent();
            info.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            info.size.height = new ComponentValue(ComponentUnit.Auto);
            info.position.y = new ComponentValue(ComponentUnit.Auto);
            info.alignment.vertical = Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            info.textAlignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            info.border.positions = [ComponentBorderPosition.Top];
            info.border.style = ComponentBorderStyle.Thin;
            AddChild(info);

            TextComponent title = new TextComponent("Settings");
            title.size.width = new ComponentValue(ComponentUnit.Auto);
            title.size.height = new ComponentValue(ComponentUnit.Fixed, 2);
            title.position.y = new ComponentValue(ComponentUnit.Auto);
            title.alignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
            title.border.positions = [ComponentBorderPosition.Bottom];
            title.border.style = ComponentBorderStyle.Thin;

            ListComponent list = new ListComponent();
            list.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            list.size.height = new ComponentValue(ComponentUnit.Auto);
            list.alignment.vertical = Components.Styles.Alignments.ComponentVerticalAlignment.Middle;
            list.OnUpdate += () => UpdateInfoText(list);

            ComponentContainer widthContainer = new ComponentContainer();
            widthContainer.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            widthContainer.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            widthContainer.position.y = new ComponentValue(ComponentUnit.Auto);

            TextComponent widthText = new TextComponent("Width ");
            widthText.size.width = new ComponentValue(ComponentUnit.Auto);
            widthText.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            widthText.position.x = new ComponentValue(ComponentUnit.Auto);

            SliderComponent widthSlider = new SliderComponent();
            widthSlider.size.width = new ComponentValue(ComponentUnit.Auto);
            widthSlider.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            widthSlider.position.x = new ComponentValue(ComponentUnit.Auto);

            widthSlider.SetMinValue(settings.GetMinWidth());
            widthSlider.SetMaxValue(settings.GetMaxWidth());
            widthSlider.SetCurrentValue(settings.GetWidth());

            widthSlider.OnChange += (slider) => settings.SetWidth(slider.GetCurrentValue());

            widthContainer.AddChild(widthText);
            widthContainer.AddChild(widthSlider);

            list.AddChild(widthContainer);

            ComponentContainer heightContainer = new ComponentContainer();
            heightContainer.size.width = new ComponentValue(ComponentUnit.Relative, 100);
            heightContainer.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            heightContainer.position.y = new ComponentValue(ComponentUnit.Auto);

            TextComponent heightText = new TextComponent("Height ");
            heightText.size.width = new ComponentValue(ComponentUnit.Auto);
            heightText.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            heightText.position.x = new ComponentValue(ComponentUnit.Auto);

            SliderComponent heightSlider = new SliderComponent();
            heightSlider.size.width = new ComponentValue(ComponentUnit.Auto);
            heightSlider.size.height = new ComponentValue(ComponentUnit.Fixed, 1);
            heightSlider.position.x = new ComponentValue(ComponentUnit.Auto);

            heightSlider.SetMinValue(settings.GetMinHeight());
            heightSlider.SetMaxValue(settings.GetMaxHeight());
            heightSlider.SetCurrentValue(settings.GetHeight());

            heightSlider.OnChange += (slider) => settings.SetHeight(slider.GetCurrentValue());

            heightContainer.AddChild(heightText);
            heightContainer.AddChild(heightSlider);

            list.AddChild(heightContainer);

            ButtonComponent backButton = new ButtonComponent("Back");
            backButton.size.width = new ComponentValue(ComponentUnit.Auto);
            backButton.size.height = new ComponentValue(ComponentUnit.Fixed, 3);
            backButton.position.y = new ComponentValue(ComponentUnit.Auto);
            backButton.alignment.horizontal = Components.Styles.Alignments.ComponentHorizontalAlignment.Center;
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

        private void UpdateInfoText(ListComponent list)
        {
            ContentString text = new ContentString();
            text.Add("Press ");
            text.Add(new ContentString("[↑]").Background(ContentColor.PURPLE));
            text.Add(" or ");
            text.Add(new ContentString("[↓]").Background(ContentColor.PURPLE));
            text.Add(" to change selection.");

            if (list.GetCurrentSelectionIndex() == 2)
            {
                text.Add("\nPress ");
                text.Add(new ContentString("[Enter]").Background(ContentColor.PURPLE));
                text.Add(" to confirm.");
            }
            else
            {
                text.Add("\nPress ");
                text.Add(new ContentString("[←]").Background(ContentColor.PURPLE));
                text.Add(" or ");
                text.Add(new ContentString("[→]").Background(ContentColor.PURPLE));
                text.Add(" to change value.");
            }

            info.SetText(text);
        }
    }
}
