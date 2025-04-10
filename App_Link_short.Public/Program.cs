
using App_Link_short.Data;
using Microsoft.EntityFrameworkCore;

namespace App_Link_short.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<ILinkService, LinkService>();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapGet("{shortCode}", async (string shortCode, ILinkService linkService) =>
            {
                var longUrl = await linkService.GetLongUrlByShortCodeAsync(shortCode);
                if (string.IsNullOrEmpty(longUrl))
                {
                    return Results.NotFound();
                }
                return Results.Redirect(longUrl);
            });

            app.Run();
        }
    }
}
