using Client.Views;
using Client.Views.Components;

namespace Client
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            ViewService viewService = new ViewService();
            ViewRoot root = new ViewRoot();
            root.size.width = 50;
            root.size.height = 12;
            root.border.style = Views.Components.Styles.Borders.ComponentBorderStyle.Thin;

            TextComponent text = new TextComponent();
            text.SetText("Test");
            text.size.widthUnit = Views.Components.Styles.ComponentUnit.Auto;
            text.size.heightUnit = Views.Components.Styles.ComponentUnit.Auto;
            text.border.style = Views.Components.Styles.Borders.ComponentBorderStyle.Round;

            text.position.x = 2;

            root.AddChild(text);

            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}