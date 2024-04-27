using Logic;

namespace Client.Views.Components.Styles
{
    public class ComponentSize : Vector2D
    {
        public ComponentUnit widthUnit;
        public ComponentUnit heightUnit;

        public int width { get => x; set => x = value; }
        public int height { get => y; set => y = value; }

        public ComponentSize() : this(0, 0) { }

        public ComponentSize(int width, int height) : base(width, height)
        {
            widthUnit = ComponentUnit.Fixed;
            heightUnit = ComponentUnit.Fixed;
        }

        public override ComponentSize Clone()
        {
            ComponentSize size = new ComponentSize(width, height);
            size.widthUnit = widthUnit;
            size.heightUnit = heightUnit;
            return size;
        }
    }
}
