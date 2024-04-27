using Logic;

namespace Client.Views.Components.Styles
{
    public class StyleSize : ICloneable
    {
        public StyleValue width;
        public StyleValue height;

        public StyleSize() : this(new StyleValue(StyleUnit.Fixed), new StyleValue(StyleUnit.Fixed)) { }

        public StyleSize(StyleValue width, StyleValue height)
        {
            this.width = width;
            this.height = height;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public StyleSize Clone()
        {
            return new StyleSize(width, height);
        }
    }
}
