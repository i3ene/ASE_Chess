using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public class ContentCharacter : ICloneable
    {
        public char character;
        public ContentColor? foregroundColor;
        public ContentColor? backgroundColor;
        public ContentStyle? style;

        public ContentCharacter(char character) : this(character, null, null, null) { }

        public ContentCharacter(char character, ContentStyle? style) : this(character, null, null, style) { }

        public ContentCharacter(char character, ContentColor? foregroundColor, ContentColor? backgroundColor) : this(character, foregroundColor, backgroundColor, null) { }

        public ContentCharacter(char character, ContentColor? foregroundColor, ContentColor? backgroundColor, ContentStyle? style)
        {
            this.character = character;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.style = style;
        }

        public ContentCharacter Foreground(ContentColor? foregroundColor)
        {
            this.foregroundColor = foregroundColor;
            return this;
        }

        public ContentCharacter Background(ContentColor? backgroundColor)
        {
            this.backgroundColor = backgroundColor;
            return this;
        }

        public ContentCharacter Style(ContentStyle? style)
        {
            this.style = style;
            return this;
        }

        public ContentCharacter Clear()
        {
            foregroundColor = null;
            backgroundColor = null;
            style = null;
            return this;
        }

        public ContentCharacter Clone()
        {
            return new ContentCharacter(character, foregroundColor, backgroundColor, style);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
