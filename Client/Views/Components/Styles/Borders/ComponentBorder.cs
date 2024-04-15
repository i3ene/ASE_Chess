using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles.Borders
{
    public class ComponentBorder
    {
        public ComponentBorderStyle style;
        public ComponentBorderPosition[] positions;
        public bool isEnabled { get => style != ComponentBorderStyle.None; }

        public ComponentBorder()
        {
            style = ComponentBorderStyle.None;
            positions = GetAllPositions();
        }

        public bool HasPosition(ComponentBorderPosition position)
        {
            return positions.Contains(position);
        }

        private static ComponentBorderPosition[] GetAllPositions()
        {
            return Enum.GetValues(typeof(ComponentBorderPosition)).Cast<ComponentBorderPosition>().ToArray();
        }
    }
}
