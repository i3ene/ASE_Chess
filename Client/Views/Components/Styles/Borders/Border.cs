namespace Client.Views.Components.Styles.Borders
{
    public class Border
    {
        public BorderStyle style;
        public BorderPosition[] positions;
        public bool isEnabled { get => style != BorderStyle.None; }

        public Border()
        {
            style = BorderStyle.None;
            positions = GetAllPositions();
        }

        public char GetPosition(BorderPosition position)
        {
            return BorderData.symbols[(int)style][(int)position];
        }

        public bool HasPosition(BorderPosition position)
        {
            return positions.Contains(position);
        }

        public bool HasAnyPosition(BorderPosition[] positions)
        {
            return this.positions.Any(position => positions.Contains(position));
        }

        public bool HasAnyTop()
        {
            return HasAnyPosition([BorderPosition.TopLeft, BorderPosition.Top, BorderPosition.TopRight]);
        }

        public bool HasAnyLeft()
        {
            return HasAnyPosition([BorderPosition.TopLeft, BorderPosition.Left, BorderPosition.BottomLeft]);
        }

        public bool HasAnyRight()
        {
            return HasAnyPosition([BorderPosition.TopRight, BorderPosition.Right, BorderPosition.BottomRight]);
        }

        public bool HasAnyBottom()
        {
            return HasAnyPosition([BorderPosition.BottomLeft, BorderPosition.Bottom, BorderPosition.BottomRight]);
        }

        private static BorderPosition[] GetAllPositions()
        {
            return Enum.GetValues(typeof(BorderPosition)).Cast<BorderPosition>().ToArray();
        }
    }
}
