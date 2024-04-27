using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
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
            info.size.width = new StyleValue(StyleUnit.Relative, 100);
            info.size.height = new StyleValue(StyleUnit.Auto);
            info.position.y = new StyleValue(StyleUnit.Auto);
            info.alignment.vertical = VerticalAlignment.Bottom;
            info.textAlignment.horizontal = HorizontalAlignment.Center;
            info.border.positions = [BorderPosition.Top];
            info.border.style = BorderStyle.Thin;
            AddChild(info);

            TextComponent title = new TextComponent("Settings");
            title.size.width = new StyleValue(StyleUnit.Auto);
            title.size.height = new StyleValue(StyleUnit.Fixed, 2);
            title.position.y = new StyleValue(StyleUnit.Auto);
            title.alignment.horizontal = HorizontalAlignment.Center;
            title.border.positions = [BorderPosition.Bottom];
            title.border.style = BorderStyle.Thin;

            ListComponent list = new ListComponent();
            list.size.width = new StyleValue(StyleUnit.Relative, 100);
            list.size.height = new StyleValue(StyleUnit.Auto);
            list.alignment.vertical = VerticalAlignment.Middle;
            list.OnUpdate += () => UpdateInfoText(list);

            ComponentContainer widthContainer = new ComponentContainer();
            widthContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            widthContainer.size.height = new StyleValue(StyleUnit.Fixed, 3);
            widthContainer.position.y = new StyleValue(StyleUnit.Auto);

            TextComponent widthText = new TextComponent("Width ");
            widthText.size.width = new StyleValue(StyleUnit.Auto);
            widthText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            widthText.position.x = new StyleValue(StyleUnit.Auto);

            SliderComponent widthSlider = new SliderComponent();
            widthSlider.size.width = new StyleValue(StyleUnit.Auto);
            widthSlider.size.height = new StyleValue(StyleUnit.Fixed, 1);
            widthSlider.position.x = new StyleValue(StyleUnit.Auto);

            widthSlider.SetMinValue(settings.GetMinWidth());
            widthSlider.SetMaxValue(settings.GetMaxWidth());
            widthSlider.SetCurrentValue(settings.GetWidth());

            widthSlider.OnChange += (slider) => settings.SetWidth(slider.GetCurrentValue());

            widthContainer.AddChild(widthText);
            widthContainer.AddChild(widthSlider);

            list.AddChild(widthContainer);

            ComponentContainer heightContainer = new ComponentContainer();
            heightContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            heightContainer.size.height = new StyleValue(StyleUnit.Fixed, 3);
            heightContainer.position.y = new StyleValue(StyleUnit.Auto);

            TextComponent heightText = new TextComponent("Height ");
            heightText.size.width = new StyleValue(StyleUnit.Auto);
            heightText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            heightText.position.x = new StyleValue(StyleUnit.Auto);

            SliderComponent heightSlider = new SliderComponent();
            heightSlider.size.width = new StyleValue(StyleUnit.Auto);
            heightSlider.size.height = new StyleValue(StyleUnit.Fixed, 1);
            heightSlider.position.x = new StyleValue(StyleUnit.Auto);

            heightSlider.SetMinValue(settings.GetMinHeight());
            heightSlider.SetMaxValue(settings.GetMaxHeight());
            heightSlider.SetCurrentValue(settings.GetHeight());

            heightSlider.OnChange += (slider) => settings.SetHeight(slider.GetCurrentValue());

            heightContainer.AddChild(heightText);
            heightContainer.AddChild(heightSlider);

            list.AddChild(heightContainer);

            ComponentContainer symbolContainer = new ComponentContainer();
            symbolContainer.size.width = new StyleValue(StyleUnit.Relative, 100);
            symbolContainer.size.height = new StyleValue(StyleUnit.Fixed, 5);
            symbolContainer.position.y = new StyleValue(StyleUnit.Auto);

            TextComponent symbolText = new TextComponent("Display Chess Symbols ");
            symbolText.size.width = new StyleValue(StyleUnit.Auto);
            symbolText.size.height = new StyleValue(StyleUnit.Fixed, 1);
            symbolText.position.x = new StyleValue(StyleUnit.Auto);
            symbolText.alignment.vertical = VerticalAlignment.Middle;
            symbolContainer.AddChild(symbolText);

            CheckboxComponent symbolCheckbox = new CheckboxComponent(settings.IsChessSymbolDisplayed());
            symbolCheckbox.position.x = new StyleValue(StyleUnit.Auto);
            symbolCheckbox.alignment.horizontal = HorizontalAlignment.Right;
            symbolCheckbox.OnSelection += (checkbox) => settings.SetChessSymbolDisplay(symbolCheckbox.IsChecked());
            symbolContainer.AddChild(symbolCheckbox);

            list.AddChild(symbolContainer);

            ButtonComponent backButton = new ButtonComponent("Back");
            backButton.size.width = new StyleValue(StyleUnit.Auto);
            backButton.size.height = new StyleValue(StyleUnit.Fixed, 3);
            backButton.position.y = new StyleValue(StyleUnit.Auto);
            backButton.alignment.horizontal = HorizontalAlignment.Center;
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
            text += "Press ";
            text += new ContentString("[↑]").Background(ContentColor.PURPLE);
            text += " or ";
            text += new ContentString("[↓]").Background(ContentColor.PURPLE);
            text += " to change selection.";

            if (list.GetCurrentSelectionIndex() == 2)
            {
                text += "\nPress ";
                text += new ContentString("[Enter]").Background(ContentColor.PURPLE);
                text += " to confirm.";
            }
            else
            {
                text += "\nPress ";
                text += new ContentString("[←]").Background(ContentColor.PURPLE);
                text += " or ";
                text += new ContentString("[→]").Background(ContentColor.PURPLE);
                text += " to change value.";
            }

            info.SetText(text);
        }
    }
}
