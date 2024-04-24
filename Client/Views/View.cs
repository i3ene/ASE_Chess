using Client.Views.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public class View : ComponentContainer
    {
        private ViewRouter router;

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
