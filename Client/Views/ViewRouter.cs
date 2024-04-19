using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
