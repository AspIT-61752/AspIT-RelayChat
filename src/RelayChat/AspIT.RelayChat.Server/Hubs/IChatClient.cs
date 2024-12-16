using AspIT.RelayChat.Entities;

namespace AspIT.RelayChat.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveNewMessage(ChatMessage chatMessage);
    }
}
