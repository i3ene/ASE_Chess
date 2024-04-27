using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public class TextComponent : Component, IDynamicDimension
    {
        public readonly Alignment textAlignment;

        private ContentString text;

        public TextComponent() : base()
        {
            textAlignment = new Alignment();
            text = new ContentString();
        }

        public TextComponent(string text) : this()
        {
            SetText(text);
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
            canvas = canvasHelper.ToCanvas(lines);

            return canvas;
        }

        private ContentString[] VerticalAlignText(ContentString[] lines)
        {
            int maxHeight = dimensionHelper.CalculateInnerHeight(this);
            int maxWidth = dimensionHelper.CalculateInnerWidth(this);

            int missing = maxHeight - lines.Length;
            List<ContentString> extendedLines = new List<ContentString>();
            for (int i = 0; i < missing; i++)
            {
                extendedLines.Add(new ContentString(new string(' ', maxWidth)));
            }

            switch (textAlignment.vertical)
            {
                case VerticalAlignment.Position:
                case VerticalAlignment.Top:
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
                case VerticalAlignment.Bottom:
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
                case VerticalAlignment.Middle:
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
            int maxWidth = dimensionHelper.CalculateInnerWidth(this);
            switch (textAlignment.horizontal)
            {
                case HorizontalAlignment.Left:
                case HorizontalAlignment.Position:
                    foreach (var line in lines)
                    {
                        line.PadRight(maxWidth);
                    }
                    break;
                case HorizontalAlignment.Right:
                    foreach (var line in lines)
                    {
                        line.PadLeft(maxWidth);
                    }
                    break;
                case HorizontalAlignment.Center:
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
            int maxWidth = dimensionHelper.CalculateInnerWidth(this);
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
                word.Add(" ");
                if (lines[index].Length == 0)
                {
                    lines[index].Add(word);
                    continue;
                }
                if ((lines[index].Length + (word.Length - 1)) > maxWidth)
                {
                    lines.Add(word);
                    continue;
                }
                lines[index].Add(word);
            }
            return lines.ToArray();
        }

        public int GetDynamicHeight()
        {
            int height = GetTextLines().Length;
            if (parent == null)
            {
                return height;
            }

            int parentHeight = dimensionHelper.CalculateInnerHeight(parent);
            return Math.Min(parentHeight, height);
        }

        public int GetDynamicWidth()
        {
            int width = text.Length;
            if (parent == null)
            {
                return width;
            }

            int parentWidth = dimensionHelper.CalculateInnerWidth(parent);
            return Math.Min(parentWidth, width);
        }
    }
}
