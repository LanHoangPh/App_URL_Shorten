using App_Link_short.Data;
using Microsoft.EntityFrameworkCore;

namespace App_Link_short.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddTransient<ILinkService, LinkService>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.MapGet("{shortCode}", async (string shortCode, ILinkService linkService) =>
            {
                Console.WriteLine($"Received shortCode: {shortCode}");
                var longUrl = await linkService.GetLongUrlByShortCodeAsync(shortCode);
                if (string.IsNullOrEmpty(longUrl))
                {
                    return Results.NotFound();
                }
                if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var uriResult) ||
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    Console.WriteLine($"Invalid URL: {longUrl}");
                    return Results.BadRequest("Invalid URL");
                }
                Console.WriteLine($"Redirecting to: {longUrl}");
                return Results.Redirect(longUrl);
            });

            app.Run();
        }
    }
}
