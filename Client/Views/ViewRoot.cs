using Client.Views.Components;

namespace Client.Views
{
    public class ViewRoot : ComponentContainer
    {
        public readonly ViewRouter router;
        private View? view;

        public ViewRoot()
        {
            router = new ViewRouter(this);
        }

        public void ChangeView(View view)
        {
            if (this.view != null) RemoveChild(this.view);
            this.view = view;
            AddChild(view);
        }
    }
}
