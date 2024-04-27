using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles
{
    public class ComponentValue
    {
        public readonly ComponentUnit unit;
        public readonly int value;

        public ComponentValue(ComponentUnit unit) : this(unit, 0) { }

        public ComponentValue(int value) : this(ComponentUnit.Auto, value) { }

        public ComponentValue(ComponentUnit unit, int value)
        {
            this.unit = unit;
            this.value = value;
        }
    }
}
