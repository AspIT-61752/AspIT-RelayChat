using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Chat.ConsoleClient
{
    public class Chat
    {
        private readonly string username;
        private readonly string hubUrl = "http://localhost:5250/chatHub";
        public event EventHandler NewMessageRecieved;
        private const string ClientHandlerName = "ReceiveNewMessage";
        private const string ServerHandlerName = "SendServerMessage";

        // The object that represents the SignalR Hub on the Server
        private readonly HubConnection connection;

        public Chat(string username)
        {
            this.username = username;

            // Makes the connection to the hub
            connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        // Starts the connection to the hub
        public async Task Start()
        {
            await connection.StartAsync();
            await Connect();
        }

        private async Task Connect()
        {
            connection.On<string, string>(ClientHandlerName, (user, message) =>
            {
                NewMessageRecieved?.Invoke(this, new MessageRecievedEventArgs() { User = user, Message = message });
            });
        }

        public async Task Send(string message)
        {
            await connection.InvokeAsync(ServerHandlerName, username, message);
        }
    }
}
