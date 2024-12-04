using AspIT.RelayChat.SignalRLibrary;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AspIT.RelayChat.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //builder.Services.AddSingleton<UsernameState>(); // This is a state container for the username
            builder.Services.AddSingleton<Chat>();

            builder.Services.AddSingleton<UsernameState>(); // This is a state container for the username

            await builder.Build().RunAsync();
        }
    }
}
