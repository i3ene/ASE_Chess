using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class TextComponent : Component, IDynamicDimension
    {
        public readonly ComponentAlignment textAlignment;

        private ContentString text;

        public TextComponent() : base()
        {
            textAlignment = new ComponentAlignment();
            text = new ContentString();
        }

        public ContentString GetText()
        {
            return text;
        }

        public void SetText(string text)
        {
            SetText(new ContentString(text));
        }

        public void SetText(ContentString text)
        {
            this.text = text;
            Update();
        }

        public override ContentCanvas GetCanvas(ContentCanvas canvas)
        {
            ContentString[] lines = GetTextLines();
            lines = HorizontalAlignText(lines);
            lines = VerticalAlignText(lines);
            canvas = canvasService.ToCanvas(lines);

            return canvas;
        }

        public ContentString[] VerticalAlignText(ContentString[] lines)
        {
            int maxHeight = dimensionService.CalculateInnerHeight(this);
            int maxWidth = dimensionService.CalculateInnerWidth(this);

            int missing = maxHeight - lines.Length;
            int top = missing / 2;
            if (top < 0)
            {
                lines = lines[Math.Abs(top)..(maxHeight + 1)];
            }
            else
            {
                List<ContentString> extendedLines = new List<ContentString>();
                for (int i = 0; i < missing; i++)
                {
                    extendedLines.Add(new ContentString(new string(' ', maxWidth)));
                }
                foreach ((ContentString line, int index) in lines.Select((line, index) => (line, index)))
                {
                    extendedLines.Insert(index + top, line);
                }
                lines = extendedLines.ToArray();
            }

            return lines;
        }

        public ContentString[] HorizontalAlignText(ContentString[] lines)
        {
            int maxWidth = dimensionService.CalculateInnerWidth(this);
            switch (textAlignment.horizontalAlignment)
            {
                case ComponentHorizontalAlignment.Left:
                case ComponentHorizontalAlignment.Positon:
                    foreach (var line in lines)
                    {
                        line.PadRight(maxWidth);
                    }
                    break;
                case ComponentHorizontalAlignment.Right:
                    foreach (var line in lines)
                    {
                        line.PadLeft(maxWidth);
                    }
                    break;
                case ComponentHorizontalAlignment.Center:
                    foreach (var line in lines)
                    {
                        int left = (maxWidth - line.Length) / 2;
                        line.PadLeft(maxWidth - left);
                        line.PadRight(maxWidth);
                    }
                    break;
            }
            return lines;
        }

        public ContentString[] GetTextLines()
        {
            int maxWidth = dimensionService.CalculateInnerWidth(this);
            ContentString[] words = text.Split(' ');
            List<ContentString> lines = new List<ContentString>();
            lines.Add(new ContentString());
            foreach (ContentString word in words)
            {
                int index = lines.Count - 1;
                if (lines[index].Length == 0)
                {
                    lines[index].Add(word);
                    continue;
                }
                if ((lines[index].Length + word.Length) > maxWidth)
                {
                    lines.Add(word);
                    continue;
                }
                lines[index].Add(" ");
                lines[index].Add(word);
            }
            return lines.ToArray();
        }

        public int GetDynamicHeight()
        {
            int width = dimensionService.CalculateInnerWidth(this);
            int height = (text.Length % width) + 1;
            if (parent == null)
            {
                return height;
            }

            int parentHeight = dimensionService.CalculateInnerHeight(parent);
            return Math.Min(parentHeight, height);
        }

        public int GetDynamicWidth()
        {
            int width = text.Length;
            if (parent == null)
            {
                return width;
            }

            int parentWidth = dimensionService.CalculateInnerWidth(parent);
            return Math.Min(parentWidth, width);
        }
    }
}
