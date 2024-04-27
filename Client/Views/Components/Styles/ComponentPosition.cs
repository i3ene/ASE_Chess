using Logic;

namespace Client.Views.Components.Styles
{
    public class ComponentPosition : ICloneable
    {
        public ComponentValue x;
        public ComponentValue y;

        public ComponentPosition() : this(new ComponentValue(ComponentUnit.Absolute), new ComponentValue(ComponentUnit.Absolute)) { }

        public ComponentPosition(ComponentValue x, ComponentValue y)
        {
            this.x = x;
            this.y = y;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public ComponentPosition Clone()
        {
            return new ComponentPosition(x, y);
        }
    }
}
