using Logic;

namespace Client.Views.Components.Styles
{
    public class ComponentPosition : Vector2D
    {
        public ComponentUnit xUnit;
        public ComponentUnit yUnit;

        public ComponentPosition() : this(0, 0) { }

        public ComponentPosition(int x, int y) : base(x, y)
        {
            xUnit = ComponentUnit.Absolute;
            yUnit = ComponentUnit.Absolute;
        }

        public override ComponentPosition Clone()
        {
            ComponentPosition position = new ComponentPosition(x, y);
            position.xUnit = xUnit;
            position.yUnit = yUnit;
            return position;
        }
    }
}
