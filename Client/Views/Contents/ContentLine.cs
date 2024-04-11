using Client.Utils;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public class ContentLine
    {
        private List<ContentCharacter> characters;

        public ContentLine() : this(string.Empty) { }

        public ContentLine(string str)
        {
            characters = new List<ContentCharacter>();
        }

        public IEnumerable<ContentCharacter> GetCharacters()
        {
            return characters.ToList();
        }

        public ContentLine Add(string str)
        {
            characters.AddRange(str.ToViewCharacters());
            return this;
        }

        public ContentLine Add(ContentLine line)
        {
            characters.AddRange(line.GetCharacters());
            return this;
        }

        public ContentLine Insert(int startIndex, string str)
        {
            characters.InsertRange(startIndex, str.ToViewCharacters());
            return this;
        }

        public ContentLine PadRight(int totalWidth)
        {
            return PadRight(totalWidth, ' ');
        }

        public ContentLine PadRight(int totalWidth, char character)
        {
            for (int i = characters.Count; i < totalWidth; i++)
            {
                characters.Add(new ContentCharacter(character));
            }
            return this;
        }

        public ContentLine PadLeft(int totalWidth)
        {
            return PadLeft(totalWidth, ' ');
        }

        public ContentLine PadLeft(int totalWidth, char character)
        {
            for (int i = characters.Count; i < totalWidth; i++)
            {
                characters.Insert(0, new ContentCharacter(character));
            }
            return this;
        }

        public static ContentLine operator +(ContentLine lhs, ContentLine rhs)
        {
            return lhs.Add(rhs);
        }

        public ContentLine Foreground(ContentColor? foregroundColor)
        {
            characters.ForEach(c => c.Foreground(foregroundColor));
            return this;
        }

        public ContentLine Background(ContentColor? backgroundColor)
        {
            characters.ForEach((c) => c.Background(backgroundColor));
            return this;
        }

        public ContentLine Reset()
        {
            characters.ForEach(c => c.Reset());
            return this;
        }
    }
}
