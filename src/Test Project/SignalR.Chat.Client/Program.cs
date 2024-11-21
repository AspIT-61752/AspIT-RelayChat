using System;
using System.Threading.Tasks;

namespace SignalR.Chat.Client
{
    internal class Program
    {
        static string username;

        static async void Main(string[] args)
        {
            await DisplayMessage("Chat", "Welcome to the chatroom!");
            await DisplayMessage("Chat", "Please enter your username:");
            username = await Console.In.ReadLineAsync();
            await DisplayMessage("Chat", $"Welcome {username}!");
            string targetChat = "AspIT ChatHub";

            try
            {
                await DisplayMessage("Chat", $"Connecting to {targetChat}");
                Chat chat = new Chat(username); // For some reason new(username) isn't working ¯\_(ツ)_/¯
                chat.NewMessageRecieved += OnNewMessageRecieved;
                await chat.Start();
                await chat.Connect();

                await DisplayMessage("Chat", $"Successfully connected to {targetChat}");

                while (true)
                {
                    string msg = await AwaitInput();
                    if (msg != String.Empty)
                    {
                        await chat.Send(msg);
                    }
                }
            }
            catch (Exception e)
            {

                await DisplayMessage("Error", $"ERROR: {e.Message}");
            }

            Console.ReadLine();
        }

        static async Task<string> AwaitInput()
        {
            await DisplayMessage(username, "> ", stayOnSameLine: true);
            string input = await Console.In.ReadLineAsync();
            return input;
        }

        static async void OnNewMessageRecieved(object sender, EventArgs e)
        {
            MessageRecievedEventArgs args = e as MessageRecievedEventArgs;
            await DisplayMessage(args.User, args.Message);
        }

        static async Task DisplayMessage(string user, string message, bool stayOnSameLine = false)
        {
            // I used this https://freeasphosting.net/date-time-format-in-c-sharp-datetime-formatting-c-sharp.html 
            string userDateTimeFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
            //var now = DateTime.Now.ToString("f");
            var now = DateTime.Now.ToString(userDateTimeFormat);
            string output = $"[{now}] - {user}> {message}";

            if (user.Equals("Server"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (user.Equals("Chat"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (user.Equals("Error"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            else if (user.Equals(username))
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            if (stayOnSameLine)
            {
                await Console.Out.WriteAsync(message);
            }
            else
            {
                await Console.Out.WriteLineAsync(output);
            }
        }
    }
}
