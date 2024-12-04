using AspIT.RelayChat.Entities;

namespace AspIT.RelayChat.SignalRLibrary
{
    public class UsernameState
    {
        public User user { get; private set; } = new User("username");
        public event Action? UserChanged;

        public void SetUsername(string username)
        {
            if (username != String.Empty)
            {
                user.Username = username;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => UserChanged?.Invoke();
    }
}
