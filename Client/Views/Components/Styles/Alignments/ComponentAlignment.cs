namespace Client.Views.Components.Styles.Alignments
{
    public class ComponentAlignment
    {
        public ComponentHorizontalAlignment horizontal { get; set; }
        public ComponentVerticalAlignment vertical { get; set; }

        public ComponentAlignment() : this(ComponentHorizontalAlignment.Position, ComponentVerticalAlignment.Position) { }

        public ComponentAlignment(ComponentHorizontalAlignment horizontal, ComponentVerticalAlignment vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }
    }
}
