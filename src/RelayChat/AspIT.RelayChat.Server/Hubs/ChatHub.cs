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

        public async Task SendServerMessage(string user, string message)
        {
            if (!users.Contains(user))
            {
                string newUserMessage = $"{user} joined the chatroom";
                _logger.Log(LogLevel.Information, newUserMessage);

                await Clients.All.ReceiveNewMessage("Server", newUserMessage);
            }

            await Clients.All.ReceiveNewMessage(user, message);
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
