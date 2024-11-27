namespace AspIT.RelayChat.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveNewMessage(string user, string message);
    }
}
