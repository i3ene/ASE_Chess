using Client.Views.Components;
using Client.Views.Components.Styles;

namespace Client.Views
{
    public class View : ComponentContainer
    {
        public ViewRouter router;

        public View(ViewRouter router) : base()
        {
            this.router = router;

            size.width = new ComponentValue(ComponentUnit.Relative, 100);
            size.height = new ComponentValue(ComponentUnit.Relative, 100);
        }

    }
}
