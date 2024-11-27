using AspIT.RelayChat.Entities;

namespace AspIT.RelayChat.SignalRLibrary
{
    public class UsernameState
    {
        public User user { get; private set; }
        public event Action? UserChanged;

        public void SetUsername(string username)
        {
            user = new User(username);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => UserChanged?.Invoke();
    }
}
