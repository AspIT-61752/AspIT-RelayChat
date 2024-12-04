using AspIT.RelayChat.Server.Hubs;
using AspIT.RelayChat.SignalRLibrary;
using AspIT.RelayChat.Web.Client.Pages;
using AspIT.RelayChat.Web.Components;

namespace AspIT.RelayChat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-9.0&pivots=server#browser-storage-server
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<Chat>();

            builder.Services.AddScoped<UsernameState>(); // This is a state container for the username

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            

            app.MapHub<ChatHub>("/chatHub");

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
