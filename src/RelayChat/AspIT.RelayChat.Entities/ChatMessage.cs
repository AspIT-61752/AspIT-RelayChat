namespace AspIT.RelayChat.Entities
{
    public class ChatMessage
    {
        private string message;
        private User sender;
        private DateTime sentTimestamp;

        public ChatMessage()
        {
        }

        public ChatMessage(string message, User sender)
        {
            this.Message = message;
            this.Sender = sender;
            sentTimestamp = DateTime.Now;
        }

        public string Message { get => message; set => message = value; }
        public User Sender { get => sender; set => sender = value; }
        public DateTime SentTimestamp { get => sentTimestamp; }
    }
}
