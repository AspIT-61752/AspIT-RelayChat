using System;

namespace SignalR.Chat.Client
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}
