using Client.Views;
using Client.Views.Components;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Components.Styles.Borders;
using Client.Views.Interactions;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ViewService viewService = new ViewService();
            ViewRoot root = new ViewRoot();
            root.size.width = 50;
            root.size.height = 16;
            root.border.style = ComponentBorderStyle.Thin;

            TextComponent text = new TextComponent();
            text.SetText("1Test\n2Test\n\n3Test");
            text.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Right;
            text.textAlignment.verticalAlignment = ComponentVerticalAlignment.Bottom;
            text.size.width = 50;
            text.size.widthUnit = ComponentUnit.Relative;
            text.size.height = 5;
            text.border.style = ComponentBorderStyle.Round;

            text.position.x = 25;
            text.position.xUnit = ComponentUnit.Relative;
            text.position.y = 1;
            text.position.yUnit = ComponentUnit.Auto;

            TextComponent text2 = new TextComponent();
            text2.SetText("4Test 5Test 6Test 7Test 8Test 9Test 10Test 11Test 12Test 13Test 14Test 15Test 16Test 17Test 18Test 19Test 20Test 21Test 22Test 23Test 24Test 25Test");
            text2.size.heightUnit = ComponentUnit.Auto;
            text2.size.widthUnit = ComponentUnit.Auto;
            text2.border.style = ComponentBorderStyle.Thick;
            text2.textAlignment.horizontalAlignment = ComponentHorizontalAlignment.Center;
            text2.position.y = 1;
            text2.position.x = 1;
            text2.position.yUnit = ComponentUnit.Auto;

            InputComponent input = new InputComponent();
            input.size.heightUnit = ComponentUnit.Auto;
            input.size.widthUnit = ComponentUnit.Relative;
            input.size.width = 100;
            input.position.yUnit = ComponentUnit.Auto;
            input.border.style = ComponentBorderStyle.Dashed;

            root.AddChild(input);
            root.AddChild(text);
            root.AddChild(text2);

            InteractionService interactionService = new InteractionService(root);
            Task interactionLoop = interactionService.ListenForInteraction();
            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}