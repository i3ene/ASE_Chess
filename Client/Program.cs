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
            text.textAlignment.horizontalAlignment = Views.Components.Styles.Alignments.ComponentHorizontalAlignment.Right;
            text.textAlignment.verticalAlignment = Views.Components.Styles.Alignments.ComponentVerticalAlignment.Bottom;
            text.size.width = 50;
            text.size.widthUnit = Views.Components.Styles.ComponentUnit.Relative;
            text.size.height = 5;
            text.border.style = Views.Components.Styles.Borders.ComponentBorderStyle.Round;

            text.position.x = 25;
            text.position.xUnit = Views.Components.Styles.ComponentUnit.Relative;
            text.position.y = 1;
            text.position.yUnit = Views.Components.Styles.ComponentUnit.Auto;

            TextComponent text2 = new TextComponent();
            text2.SetText("4Test");
            text2.size.heightUnit = Views.Components.Styles.ComponentUnit.Auto;
            text2.size.widthUnit = Views.Components.Styles.ComponentUnit.Auto;
            text2.border.style = Views.Components.Styles.Borders.ComponentBorderStyle.Thick;
            text2.position.y = 1;
            text2.position.yUnit = Views.Components.Styles.ComponentUnit.Auto;

            root.AddChild(text);
            root.AddChild(text2);

            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}