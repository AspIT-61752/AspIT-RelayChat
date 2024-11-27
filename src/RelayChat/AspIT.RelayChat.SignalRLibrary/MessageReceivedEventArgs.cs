namespace AspIT.RelayChat.SignalRLibrary
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}
