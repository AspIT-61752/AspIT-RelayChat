using AspIT.RelayChat.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace AspIT.RelayChat.SignalRLibrary
{
    public class Chat
    {
        public UsernameState usernameState { get; set; } = new();
        private readonly string hubUrl = "http://localhost:5270/chatHub";
        public event EventHandler NewMessageReceived;
        private const string ClientHandlerName = "ReceiveNewMessage";
        private const string ServerHandlerName = "SendServerMessage";

        public readonly HubConnection hubConnection;

        public Chat()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            //this.currentUser = new User("test user");
        }

        public Chat(string username)
        {
            this.usernameState.SetUsername(username);

            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        public async Task Start()
        {
            await hubConnection.StartAsync();
            await Connect();
        }

        private async Task Connect()
        {
            hubConnection.On<ChatMessage>(ClientHandlerName, chatMessage =>
            {
                Console.WriteLine($"Message received from {chatMessage.Sender.Username}: {chatMessage.Message}");

                NewMessageReceived?.Invoke(this, new MessageReceivedEventArgs() { User = chatMessage.Sender.Username, Message = chatMessage.Message });
            });
        }

        public async Task SendMessage(ChatMessage msg)
        {
            await hubConnection.InvokeAsync(ServerHandlerName, msg);
        }
    }
}
