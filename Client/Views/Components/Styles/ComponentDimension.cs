using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles
{
    public class ComponentDimension
    {
        public readonly ComponentPosition position;
        public readonly ComponentSize innerSize;
        public readonly ComponentSize outerSize;

        public ComponentDimension() : this(new ComponentPosition(), new ComponentSize(), new ComponentSize()) { }

        public ComponentDimension(ComponentPosition position, ComponentSize innerSize, ComponentSize outerSize)
        {
            this.innerSize = innerSize;
            this.outerSize = outerSize;
            this.position = position;
        }
    }
}
