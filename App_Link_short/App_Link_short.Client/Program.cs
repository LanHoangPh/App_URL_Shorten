using App_Link_short.Client.Interfaces;
using App_Link_short.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace App_Link_short.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
            builder.Services.AddTransient<ILinkService, LinkApiProxy>();
            builder.Services.AddRefitClient<ILinkApi>().ConfigureHttpClient(client =>
            {
                var apiUrl = builder.HostEnvironment.BaseAddress; 
                client.BaseAddress = new Uri(apiUrl);
            } );
            builder.Services.AddScoped<SessionStorage>();

            await builder.Build().RunAsync();
        }
    }
}
