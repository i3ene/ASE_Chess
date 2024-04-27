namespace Client.Communications
{
    public class ChatMessage
    {
        public readonly string sender;
        public readonly string message;

        public ChatMessage(string message) : this(string.Empty, message) { }

        public ChatMessage(string sender, string message)
        {
            this.sender = sender;
            this.message = message;
        }
    }
}
