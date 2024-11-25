namespace AspIT.RelayChat.Entities
{
    public class User
    {
        private string username;
        //private string password;
        //private string email;

        public User()
        {
        }
        public User(string username)
        {
            this.Username = username;
        }

        //public User(string username, string password, string email)
        //{
        //    this.Username = username;
        //    this.Password = password;
        //    this.Email = email;
        //}

        public string Username { get => username; set => username = value; }
        //public string Password { get => password; set => password = value; }
        //public string Email { get => email; set => email = value; }
    }
}