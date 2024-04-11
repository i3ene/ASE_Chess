using Client.Views.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class RootView : ComponentContainer
    {
        private readonly ViewRouter router;
        private View? view;

        public RootView()
        {
            router = new ViewRouter(this);
        }

        public void ChangeView(View view)
        {
            this.view = view;
        }
    }
}
