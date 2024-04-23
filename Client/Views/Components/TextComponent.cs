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

        private ContentString[] VerticalAlignText(ContentString[] lines)
        {
            int maxHeight = dimensionService.CalculateInnerHeight(this);
            int maxWidth = dimensionService.CalculateInnerWidth(this);

            int missing = maxHeight - lines.Length;
            List<ContentString> extendedLines = new List<ContentString>();
            for (int i = 0; i < missing; i++)
            {
                extendedLines.Add(new ContentString(new string(' ', maxWidth)));
            }

            switch (textAlignment.verticalAlignment)
            {
                case ComponentVerticalAlignment.Position:
                case ComponentVerticalAlignment.Top:
                    if (missing < 0)
                    {
                        lines = lines[..maxHeight];
                    }
                    else
                    {
                        foreach (ContentString line in lines)
                        {
                            extendedLines.Add(line);
                        }
                        lines = extendedLines.ToArray();
                    }
                    break;
                case ComponentVerticalAlignment.Bottom:
                    if (missing < 0)
                    {
                        lines = lines[^maxHeight..];
                    }
                    else
                    {
                        foreach (ContentString line in lines)
                        {
                            extendedLines.Add(line);
                        }
                        lines = extendedLines.ToArray();
                    }
                    break;
                case ComponentVerticalAlignment.Middle:
                    int top = missing / 2;
                    if (missing < 0)
                    {
                        lines = lines[Math.Abs(top)..(maxHeight + 1)];
                    }
                    else
                    {
                        foreach ((ContentString line, int index) in lines.Select((line, index) => (line, index)))
                        {
                            extendedLines.Insert(index + top, line);
                        }
                        lines = extendedLines.ToArray();
                    }
                    break;
            }

            return lines;
        }

        private ContentString[] HorizontalAlignText(ContentString[] lines)
        {
            int maxWidth = dimensionService.CalculateInnerWidth(this);
            switch (textAlignment.horizontalAlignment)
            {
                case ComponentHorizontalAlignment.Left:
                case ComponentHorizontalAlignment.Position:
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

        private ContentString[] GetTextLines()
        {
            int maxWidth = dimensionService.CalculateInnerWidth(this);
            ContentString[] words = text.Split(' ')
                .Select(word => 
                    // Split every "New Line" Character
                    word.Split('\n')
                    .Select(word => 
                        // Add single "New Line" Characters in between each array element
                        new ContentString[2] { word, new ContentString("\n") })
                        // Flatten the array
                        .SelectMany(word => word)
                        // Remove last "New Line" Character
                        .ToArray()[..^1])
                    // Flatten the array
                    .SelectMany(word => word)
                .ToArray();
            List<ContentString> lines = new List<ContentString>();
            lines.Add(new ContentString());
            foreach (ContentString word in words)
            {
                int index = lines.Count - 1;
                if (word.Length == 1 && word[0].character == '\n')
                {
                    lines.Add(new ContentString());
                    continue;
                }
                if (lines[index].Length == 0)
                {
                    lines[index].Add(word);
                    continue;
                }
                if ((lines[index].Length + word.Length) >= maxWidth)
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
            int height = GetTextLines().Length;
            if (border.HasAnyTop()) height += 1;
            if (border.HasAnyBottom()) height += 1;
            if (parent == null)
            {
                return height;
            }

            int parentHeight = dimensionService.CalculateInnerHeight(parent);
            int parentYPosition = dimensionService.CalculateYPosition(parent);
            int yPosition = dimensionService.CalculateYPosition(this);
            yPosition -= parentYPosition;
            parentHeight -= yPosition;
            return Math.Min(parentHeight, height);
        }

        public int GetDynamicWidth()
        {
            int width = text.Length;
            if (border.HasAnyLeft()) width += 1;
            if (border.HasAnyRight()) width += 1;
            if (parent == null)
            {
                return width;
            }

            int parentWidth = dimensionService.CalculateInnerWidth(parent);
            int parentXPosition = dimensionService.CalculateXPosition(parent);
            int xPosition = dimensionService.CalculateXPosition(this);
            xPosition -= parentXPosition;
            parentWidth -= xPosition;
            return Math.Min(parentWidth, width);
        }
    }
}
