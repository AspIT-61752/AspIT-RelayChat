using AspIT.RelayChat.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace AspIT.RelayChat.SignalRLibrary
{
    public class Chat
    {
        public readonly User currentUser;
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
        }

        public Chat(string username)
        {
            this.currentUser = new User(username);

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
            hubConnection.On<string, string>(ClientHandlerName, (user, message) =>
            {
                NewMessageReceived?.Invoke(this, new MessageReceivedEventArgs() { User = user, Message = message });
            });
        }

        public async Task SendMessage(string message)
        {
            await hubConnection.InvokeAsync(ServerHandlerName, currentUser.Username, message);
        }
    }
}
