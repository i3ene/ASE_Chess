namespace Client.Views.Components.Styles.Alignments
{
    public class ComponentAlignment
    {
        public ComponentHorizontalAlignment horizontalAlignment { get; set; }
        public ComponentVerticalAlignment verticalAlignment { get; set; }

        public ComponentAlignment() : this(ComponentHorizontalAlignment.Position, ComponentVerticalAlignment.Position) { }

        public ComponentAlignment(ComponentHorizontalAlignment horizontalAlignment, ComponentVerticalAlignment verticalAlignment)
        {
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
        }
    }
}
