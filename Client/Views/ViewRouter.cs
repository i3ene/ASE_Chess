using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class ViewRouter
    {
        private readonly RootView root;

        public ViewRouter(RootView root)
        {
            this.root = root;
        }

        public void Display(View view)
        {
            root.ChangeView(view);
        }
    }
}
