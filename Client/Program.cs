using Client.Views;
using Client.Views.Components.Styles.Borders;
using Client.Views.Interactions;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Settings settings = Settings.getInstance();

            ViewService viewService = new ViewService();
            ViewRoot root = new ViewRoot();
            root.size.width = settings.GetWidth();
            root.size.height = settings.GetHeight();
            root.border.style = ComponentBorderStyle.Thin;

            settings.OnPropertyChange += (settings, property) =>
            {
                root.size.width = settings.GetWidth();
                root.size.height = settings.GetHeight();
                root.Update();
            };

            MenuView menu = new MenuView(root.router);
            root.ChangeView(menu);

            InteractionService interactionService = new InteractionService(root);
            Task interactionLoop = interactionService.ListenForInteraction();
            Task viewLoop = viewService.Display(root);
            viewLoop.Wait();
        }
    }
}