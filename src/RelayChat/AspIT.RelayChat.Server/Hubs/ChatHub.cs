using AspIT.RelayChat.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AspIT.RelayChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        static List<string> users = new();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SendServerMessage(ChatMessage chatMessage)
        {
            _logger.LogInformation($"SendServerMessage called with user: {chatMessage.Sender.Username}, message: {chatMessage.Message}");

            if (!users.Contains(chatMessage.Sender.Username))
            {
                string newUserMessage = $"{chatMessage.Sender.Username} joined the chatroom"; // Debugging purposes
                _logger.Log(LogLevel.Information, newUserMessage); // Debugging purposes

                users.Add(chatMessage.Sender.Username);
                await Clients.All.ReceiveNewMessage(new ChatMessage { Sender = new User {Username = "Server" }, Message = newUserMessage });
            }

            await Clients.All.ReceiveNewMessage(chatMessage);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"New connection: {Context.ConnectionId}"); // Debugging purposes
            //await Clients.Client(Context.ConnectionId).ReceiveNewMessage(new ChatMessage { Sender = new User { Username = Context.ConnectionId }, Message = "Welcome to the chatroom!" } ); // Debugging purposes
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Connection closed: {Context.ConnectionId} => {exception.Message}"); // Debugging purposes
            await base.OnDisconnectedAsync(exception);
        }
    }
}
