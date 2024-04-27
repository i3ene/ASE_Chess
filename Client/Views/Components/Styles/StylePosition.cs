using Logic;

namespace Client.Views.Components.Styles
{
    public class StylePosition : ICloneable
    {
        public StyleValue x;
        public StyleValue y;

        public StylePosition() : this(new StyleValue(StyleUnit.Absolute), new StyleValue(StyleUnit.Absolute)) { }

        public StylePosition(StyleValue x, StyleValue y)
        {
            this.x = x;
            this.y = y;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public StylePosition Clone()
        {
            return new StylePosition(x, y);
        }
    }
}
