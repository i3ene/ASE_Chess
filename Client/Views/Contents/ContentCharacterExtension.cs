namespace Client.Views.Contents
{
    public static class ContentCharacterExtension
    {
        public static ContentCharacter[] ToContentCharacters(this string str)
        {
            return str.ToCharArray().Select(c => new ContentCharacter(c)).ToArray();
        }
    }
}
