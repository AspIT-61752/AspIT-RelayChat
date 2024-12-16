using AspIT.RelayChat.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace AspIT.RelayChat.SignalRLibrary
{
    public class Chat
    {
        public UsernameState usernameState { get; set; } = new(); // The statecontainer for the username
        private readonly string hubUrl = "http://localhost:5270/chatHub"; // The URL of the hub
        public event EventHandler NewMessageReceived;

        // The name of the method in the hub that the client will call
        private const string ClientHandlerName = "ReceiveNewMessage"; // Receives a message from the server
        private const string ServerHandlerName = "SendServerMessage"; // Sends a message to the server

        public readonly HubConnection hubConnection; // readonly because it should not be changed after it has been set

        public Chat()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        /// <summary>
        /// Sets the username of the user and creates a connection to the hub
        /// </summary>
        /// <param name="username"></param>
        public Chat(string username)
        {
            this.usernameState.SetUsername(username);

            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        /// <summary>
        /// Starts the connection to the hub and connects to the hub
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            await hubConnection.StartAsync();
            await Connect();
        }

        /// <summary>
        /// Connects to the hub
        /// </summary>
        /// <returns></returns>
        private async Task Connect()
        {
            hubConnection.On<ChatMessage>(ClientHandlerName, chatMessage =>
            {
                Console.WriteLine($"Message received from {chatMessage.Sender.Username}: {chatMessage.Message}");

                NewMessageReceived?.Invoke(this, new MessageReceivedEventArgs() { User = chatMessage.Sender.Username, Message = chatMessage.Message });
            });
        }

        /// <summary>
        /// Sends a message to the hub. The hub will then broadcast the message to all connected clients
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task SendMessage(ChatMessage msg)
        {
            await hubConnection.InvokeAsync(ServerHandlerName, msg);
        }
    }
}
