using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Contents
{
    public static class ContentStringExtension
    {
        public static ContentString ToContentString(this string str)
        {
            return new ContentString(str.ToContentCharacters());
        }
    }
}
