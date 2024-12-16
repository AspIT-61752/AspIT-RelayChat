using AspIT.RelayChat.Entities;

namespace AspIT.RelayChat.Server.Hubs
{
    public interface IChatClient
    {
        /// <summary>
        /// Occurs when a new message is received
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        Task ReceiveNewMessage(ChatMessage chatMessage);
    }
}
