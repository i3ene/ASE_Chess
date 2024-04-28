namespace Client.Communications
{
    public class ChatRepository
    {
        private readonly List<ChatMessage> messages;

        public delegate void CollectionChanged(ChatMessage newMessage);
        public event CollectionChanged? OnChange;

        public ChatRepository()
        {
            messages = new List<ChatMessage>();
        }

        public void AddMessage(ChatMessage message)
        {
            messages.Add(message);
            OnChange?.Invoke(message);
        }

        public ChatMessage[] GetAllMessages()
        {
            return messages.ToArray();
        }
    }
}
