namespace Client
{
    public class Settings
    {
        private static readonly Settings instance;

        public delegate void SettingsPropertyHandler(Settings sender, string property);
        public event SettingsPropertyHandler? OnPropertyChange;

        private int minWidth;
        private int width;
        private int maxWidth;

        private int minHeight;
        private int height;
        private int maxHeight;

        private bool displayChessSymbols;

        static Settings()
        {
            instance = new Settings();
        }

        private Settings() 
        {
            maxWidth = Math.Max(160, Console.WindowWidth);
            minWidth = 30;
            width = Console.WindowWidth;

            maxHeight = Math.Max(40, Console.WindowHeight);
            minHeight = 21;
            height = Console.WindowHeight;

            displayChessSymbols = false;
        }

        public bool IsChessSymbolDisplayed()
        {
            return displayChessSymbols;
        }

        public bool SetChessSymbolDisplay(bool displayChessSymbols)
        {
            this.displayChessSymbols = displayChessSymbols;
            OnPropertyChange?.Invoke(this, "displayChessSymbols");
            return this.displayChessSymbols;
        }

        public int GetWidth()
        {
            return width;
        }

        public int SetWidth(int width)
        {
            this.width = Math.Min(maxWidth, Math.Max(minWidth, width));
            OnPropertyChange?.Invoke(this, "width");
            return this.width;
        }

        public int GetMaxWidth()
        {
            return maxWidth;
        }

        public int GetMinWidth()
        {
            return minWidth;
        }

        public int GetHeight()
        {
            return height;
        }

        public int SetHeight(int height)
        {
            this.height = Math.Min(maxHeight, Math.Max(minHeight, height));
            OnPropertyChange?.Invoke(this, "height");
            return this.height;
        }

        public int GetMaxHeight()
        {
            return maxHeight;
        }

        public int GetMinHeight()
        {
            return minHeight;
        }

        public static Settings getInstance()
        {
            return instance;
        }
    }
}
