﻿namespace Client.Communications
{
    public class ChatRepository
    {
        private readonly List<ChatMessage> messages;

        public delegate void CollectionChanged();
        public event CollectionChanged? OnChange;

        public ChatRepository()
        {
            messages = new List<ChatMessage>();
        }

        public void AddMessage(ChatMessage message)
        {
            messages.Add(message);
            OnChange?.Invoke();
        }

        public ChatMessage[] GetAllMessages()
        {
            return messages.ToArray();
        }
    }
}
