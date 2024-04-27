namespace Client.Views
{
    public class ViewRouter
    {
        private readonly ViewRoot root;

        public ViewRouter(ViewRoot root)
        {
            this.root = root;
        }

        public void Display(View view)
        {
            root.ChangeView(view);
        }
    }
}
