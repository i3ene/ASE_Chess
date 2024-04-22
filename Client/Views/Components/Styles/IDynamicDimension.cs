using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles
{
    public interface IDynamicDimension
    {
        public int GetDynamicWidth();
        public int GetDynamicHeight();
    }
}
