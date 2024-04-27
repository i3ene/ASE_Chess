using Logic;

namespace Client.Views.Components.Styles
{
    public class ComponentSize : ICloneable
    {
        public ComponentValue width;
        public ComponentValue height;

        public ComponentSize() : this(new ComponentValue(ComponentUnit.Fixed), new ComponentValue(ComponentUnit.Fixed)) { }

        public ComponentSize(ComponentValue width, ComponentValue height)
        {
            this.width = width;
            this.height = height;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public ComponentSize Clone()
        {
            return new ComponentSize(width, height);
        }
    }
}
