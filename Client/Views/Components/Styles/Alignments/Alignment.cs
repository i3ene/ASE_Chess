namespace Client.Views.Components.Styles.Alignments
{
    public class Alignment
    {
        public HorizontalAlignment horizontal { get; set; }
        public VerticalAlignment vertical { get; set; }

        public Alignment() : this(HorizontalAlignment.Position, VerticalAlignment.Position) { }

        public Alignment(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }
    }
}
