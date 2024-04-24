using Client.Communications;
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
            root.size.width = 80;
            root.size.height = 26;
            root.border.style = ComponentBorderStyle.Thin;

            MenuView menu = new MenuView(root.router);
            root.ChangeView(menu);

            InteractionService interactionService = new InteractionService(root);
            Task interactionLoop = interactionService.ListenForInteraction();
            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}