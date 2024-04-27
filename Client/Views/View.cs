using Client.Views.Components;

namespace Client.Views
{
    public class View : ComponentContainer
    {
        public ViewRouter router;

        public View(ViewRouter router) : base()
        {
            this.router = router;

            size.widthUnit = Components.Styles.ComponentUnit.Relative;
            size.width = 100;
            size.heightUnit = Components.Styles.ComponentUnit.Relative;
            size.height = 100;
        }

    }
}
