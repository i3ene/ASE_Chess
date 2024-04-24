using Client.Views.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
