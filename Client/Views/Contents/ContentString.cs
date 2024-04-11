using Client.Utils;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public class ContentString
    {
        private List<ContentCharacter> characters;

        public ContentString() : this(string.Empty) { }

        public ContentString(string str)
        {
            characters = new List<ContentCharacter>();
        }

        public IEnumerable<ContentCharacter> GetCharacters()
        {
            return characters.ToList();
        }

        public ContentString Add(string str)
        {
            characters.AddRange(str.ToViewCharacters());
            return this;
        }

        public ContentString Add(ContentString line)
        {
            characters.AddRange(line.GetCharacters());
            return this;
        }

        public ContentString Insert(int startIndex, string str)
        {
            characters.InsertRange(startIndex, str.ToViewCharacters());
            return this;
        }

        public ContentString PadRight(int totalWidth)
        {
            return PadRight(totalWidth, ' ');
        }

        public ContentString PadRight(int totalWidth, char character)
        {
            for (int i = characters.Count; i < totalWidth; i++)
            {
                characters.Add(new ContentCharacter(character));
            }
            return this;
        }

        public ContentString PadLeft(int totalWidth)
        {
            return PadLeft(totalWidth, ' ');
        }

        public ContentString PadLeft(int totalWidth, char character)
        {
            for (int i = characters.Count; i < totalWidth; i++)
            {
                characters.Insert(0, new ContentCharacter(character));
            }
            return this;
        }

        public static ContentString operator +(ContentString lhs, ContentString rhs)
        {
            return lhs.Add(rhs);
        }

        public ContentString Foreground(ContentColor? foregroundColor)
        {
            characters.ForEach(c => c.Foreground(foregroundColor));
            return this;
        }

        public ContentString Background(ContentColor? backgroundColor)
        {
            characters.ForEach((c) => c.Background(backgroundColor));
            return this;
        }

        public ContentString Reset()
        {
            characters.ForEach(c => c.Reset());
            return this;
        }
    }
}
