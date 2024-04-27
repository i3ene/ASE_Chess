namespace Logic.Communications.Actions
{
    public class MessageAction : Action
    {
        public string sender { get; set; }
        public string message {  get; set; }

        public MessageAction() : this(string.Empty) { }

        public MessageAction(string message) : this(string.Empty, message) { }

        public MessageAction(string sender, string message) : base(ActionType.Message)
        {
            this.sender = sender;
            this.message = message;
        }
    }
}
