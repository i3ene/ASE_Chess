﻿using Client.Utils;
using Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public class ContentString : ICloneable, IEnumerable<ContentCharacter>
    {
        private List<ContentCharacter> characters;

        public ContentCharacter this[int i]
        {
            get { return characters[i]; }
            set { characters[i] = value; }
        }
        public int Length => characters.Count;

        public ContentString() : this(string.Empty) { }

        public ContentString(string str) : this(str.ToContentCharacters()) { }

        public ContentString(ContentCharacter[] characters)
        {
            this.characters = new List<ContentCharacter>(characters);
        }

        public IEnumerable<ContentCharacter> GetCharacters()
        {
            return characters.ToList();
        }

        public ContentString Add(string str)
        {
            characters.AddRange(str.ToContentCharacters());
            return this;
        }

        public ContentString Add(ContentString line)
        {
            characters.AddRange(line.Clone().GetCharacters());
            return this;
        }

        public ContentString Insert(int startIndex, string str)
        {
            characters.InsertRange(startIndex, str.ToContentCharacters());
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

        public ContentString Substring(int startIndex)
        {
            int length = characters.Count - startIndex;
            return Substring(startIndex, length);
        }

        public ContentString Substring(int startIndex, int length)
        {
            int endIndex = startIndex + length;
            ContentCharacter[] substring = Clone().characters.Where((c, i) => i >= startIndex && i <= endIndex).ToArray();
            return new ContentString(substring);
        }

        public static ContentString operator +(ContentString lhs, ContentString rhs)
        {
            return lhs.Clone().Add(rhs.Clone());
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

        public ContentString Clear()
        {
            characters.ForEach(c => c.Clear());
            return this;
        }

        public ContentString Clone()
        {
            return new ContentString(characters.Select(c => c.Clone()).ToArray());
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public IEnumerator<ContentCharacter> GetEnumerator()
        {
            return new ContentStringEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}