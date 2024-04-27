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
