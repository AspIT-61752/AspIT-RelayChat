using AspIT.RelayChat.Entities;
using Microsoft.AspNetCore.SignalR;

namespace AspIT.RelayChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        static List<string> users = new(); // List of users in the chatroom
        private readonly ILogger<ChatHub> _logger; // Logger for debugging purposes

        /// <summary>
        /// On construction, the ChatHub is injected with a logger
        /// </summary>
        /// <param name="logger"></param>
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sends a message to all clients in the chatroom. If the sender is not already in the chatroom, they are added and all clients are notified with a message.
        /// </summary>
        /// <param name="chatMessage">The ChatMessage with the username</param>
        /// <returns></returns>
        public async Task SendServerMessage(ChatMessage chatMessage)
        {
            // Debugging purposes
            _logger.LogInformation($"SendServerMessage called with user: {chatMessage.Sender.Username}, message: {chatMessage.Message}");

            // If the user is not already in the chatroom, add them and notify all clients with a message
            if (!users.Contains(chatMessage.Sender.Username))
            {
                string newUserMessage = $"{chatMessage.Sender.Username} joined the chatroom"; // Debugging purposes
                _logger.Log(LogLevel.Information, newUserMessage); // Debugging purposes

                users.Add(chatMessage.Sender.Username); // Add the user to the chatroom
                await Clients.All.ReceiveNewMessage(new ChatMessage { Sender = new User {Username = "Server" }, Message = newUserMessage }); // Notify all clients with a message
            }

            // Send the message to all clients
            await Clients.All.ReceiveNewMessage(chatMessage);
        }

        /// <summary>
        /// When a new connection is established, this method is called
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"New connection: {Context.ConnectionId}"); // Debugging purposes
            //await Clients.Client(Context.ConnectionId).ReceiveNewMessage(new ChatMessage { Sender = new User { Username = Context.ConnectionId }, Message = "Welcome to the chatroom!" } ); // Debugging purposes
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// When a connection is closed, this method is called. It is also called when the connection is lost due to networking issues
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Connection closed: {Context.ConnectionId} => {exception.Message}"); // Debugging purposes
            await base.OnDisconnectedAsync(exception);
        }
    }
}
