using App_Link_short.Data;

namespace App_Link_short.Services;

public interface IShortCodeGeneratorService
{
    Task<string> GenerateShortCodeAsync();
}

public class ShortCodeGeneratorService : IShortCodeGeneratorService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public ShortCodeGeneratorService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task<string> GenerateShortCodeAsync()
    {
        var shortCode = GenerateShortCode(6);
        await using var dbContext = _dbContextFactory.CreateDbContext();
        while (await dbContext.Links.AsNoTracking().AnyAsync(l=>l.ShortCode == shortCode))
        {
            shortCode = GenerateShortCode(6);
        }
        return shortCode;
    }

    private static string GenerateShortCode(int length)
    {
        const string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int availableLenght = availableChars.Length;
        var shortCodeChars = Enumerable.Repeat(availableChars, length)
            .Select(s =>
            {
                var randomNumber = Random.Shared.Next(availableLenght);
                return s[randomNumber];
            }).ToArray();
        return new string(shortCodeChars);
    }
}