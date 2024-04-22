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
            text.SetText("1Test\n2Test\n\n3Test");
            text.alignment.horizontalAlignment = Views.Components.Styles.Alignments.ComponentHorizontalAlignment.Right;
            text.alignment.verticalAlignment = Views.Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            text.size.width = 50;
            text.size.widthUnit = Views.Components.Styles.ComponentUnit.Relative;
            text.size.height = 5;
            text.border.style = Views.Components.Styles.Borders.ComponentBorderStyle.Round;

            text.position.x = 25;
            text.position.xUnit = Views.Components.Styles.ComponentUnit.Relative;

            root.AddChild(text);

            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}