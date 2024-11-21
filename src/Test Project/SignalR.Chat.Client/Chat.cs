//using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalR.Chat.Client
{
    public class Chat
    {
        string username;
        string hubUrl = "http://localhost:5250/chatHub";
        public event EventHandler NewMessageRecieved;
        const string ClientHandlerName = "ReceiveNewMessage";
        const string ServerHandlerName = "SendServerMessage";

        // The object that represents the SignalR Hub on the Server
        HubConnection connection;

        public Chat(string username)
        {
            this.username = username;

            // Makes the connection to the hub
            connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        // Starts the connection to the hub
        public async Task Start() => await connection.StartAsync();

        public async Task Connect()
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
