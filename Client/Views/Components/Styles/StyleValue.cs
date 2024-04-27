using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles
{
    public class StyleValue
    {
        public readonly StyleUnit unit;
        public readonly int value;

        public StyleValue(StyleUnit unit) : this(unit, 0) { }

        public StyleValue(int value) : this(StyleUnit.Auto, value) { }

        public StyleValue(StyleUnit unit, int value)
        {
            this.unit = unit;
            this.value = value;
        }
    }
}
