namespace SignalR.Chat.Hubs
{
    public interface IChatClient
    {
        Task ReceiveNewMessage(string user, string message);
    }
}
