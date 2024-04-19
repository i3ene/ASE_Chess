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

        public char GetPosition(ComponentBorderPosition position)
        {
            return BorderData.symbols[(int)style][(int)position];
        }

        public bool HasPosition(ComponentBorderPosition position)
        {
            return positions.Contains(position);
        }

        public bool HasAnyPosition(ComponentBorderPosition[] positions)
        {
            return this.positions.Any(position => positions.Contains(position));
        }

        public bool HasAnyTop()
        {
            return HasAnyPosition([ComponentBorderPosition.TopLeft, ComponentBorderPosition.Top, ComponentBorderPosition.TopRight]);
        }

        public bool HasAnyLeft()
        {
            return HasAnyPosition([ComponentBorderPosition.TopLeft, ComponentBorderPosition.Left, ComponentBorderPosition.BottomLeft]);
        }

        public bool HasAnyRight()
        {
            return HasAnyPosition([ComponentBorderPosition.TopRight, ComponentBorderPosition.Right, ComponentBorderPosition.BottomRight]);
        }

        public bool HasAnyBottom()
        {
            return HasAnyPosition([ComponentBorderPosition.BottomLeft, ComponentBorderPosition.Bottom, ComponentBorderPosition.BottomRight]);
        }

        private static ComponentBorderPosition[] GetAllPositions()
        {
            return Enum.GetValues(typeof(ComponentBorderPosition)).Cast<ComponentBorderPosition>().ToArray();
        }
    }
}
