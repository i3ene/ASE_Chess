namespace Client.Views.Components.Styles
{
    public class StyleDimension
    {
        public readonly StylePosition position;
        public readonly StyleSize innerSize;
        public readonly StyleSize outerSize;

        public StyleDimension() : this(new StylePosition(), new StyleSize(), new StyleSize()) { }

        public StyleDimension(StylePosition position, StyleSize innerSize, StyleSize outerSize)
        {
            this.innerSize = innerSize;
            this.outerSize = outerSize;
            this.position = position;
        }
    }
}
