using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SignalR.Chat.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        static List<string> users = new();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
            //logger.Log(LogLevel.Information, "Hub created"); // This made me think that the hub was created multiple times, but it is not
        }

        public async Task SendServerMessage(string user, string message)
        {
            // If the user is not in the list, add it
            if (!users.Contains(user))
            {
                string newUserMsg = $"{user} joined the chatroom";
                users.Add(user);
                _logger.Log(LogLevel.Information, newUserMsg);
                await Clients.All.ReceiveNewMessage("Server", newUserMsg);
            }

            await Clients.Others.ReceiveNewMessage(user, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"New connection: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Connection closed: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

    }
}
