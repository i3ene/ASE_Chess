using Client.Views.Components;
using Client.Views.Components.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static Settings()
        {
            instance = new Settings();
        }

        private Settings() 
        {
            maxWidth = Math.Max(160, Console.WindowWidth - 1);
            minWidth = 30;
            width = Console.WindowWidth - 1;

            maxHeight = Math.Max(40, Console.WindowHeight - 1);
            minHeight = 20;
            height = Console.WindowHeight - 1;
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
