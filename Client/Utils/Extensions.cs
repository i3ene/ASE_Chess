using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utils
{
    public static class Extensions
    {
        public static ContentCharacter[] ToViewCharacters(this string str)
        {
            return str.ToCharArray().Select(c => new ContentCharacter(c)).ToArray();
        }
    }
}
