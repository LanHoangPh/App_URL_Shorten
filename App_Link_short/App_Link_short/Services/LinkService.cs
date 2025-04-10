namespace App_Link_short.Services;

public class LinkService : ILinkService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IShortCodeGeneratorService _shortCodeGeneratorService;
    private readonly IConfiguration _configuration;
    public LinkService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IShortCodeGeneratorService shortCodeGeneratorService, IConfiguration configuration)
    {
        _dbContextFactory = dbContextFactory;
        _shortCodeGeneratorService = shortCodeGeneratorService;
        _configuration = configuration;
    }

    public async Task<LinkDto> GenerateLinkAsync(LinkCreateDto dto)
    {
        var domain = _configuration["Domain"] ?? throw new InvalidOperationException($"Domain not configured");
        var shortCode = await _shortCodeGeneratorService.GenerateShortCodeAsync();
        var link = new Link
        {
            LongUrl = dto.LongUrl,
            UserId = dto.UserId,
            ShortUrl = $"{domain.TrimEnd('/')}/{shortCode}", //Https://localhost:5000/{shortCode}
            ShortCode = shortCode,
            IsActive = true
            
        };
        await using var db = _dbContextFactory.CreateDbContext();
        db.Links.Add(link);
        await db.SaveChangesAsync();
        return new LinkDto
        {
            Id = link.Id,
            LongUrl = link.LongUrl,
            IsActive = true,
            ShortUrl = link.ShortUrl
        };
    }
    
    public async Task<PagedResult<LinkDto>> GetLinksByUserAsync(string userId, int strartIndex, int pageSize, bool activeOnly)
    {
        await using var db = _dbContextFactory.CreateDbContext();
        var query = db.Links.Where(l => l.UserId == userId);
        if (activeOnly)
        {
            query = query.Where(l => l.IsActive);
        }
        var totalCount = await query.CountAsync();
        var links = await query.Skip(strartIndex).Take(pageSize).Select(l=> new LinkDto
        {
            Id = l.Id,
            LongUrl = l.LongUrl,
            IsActive = l.IsActive,
            ShortUrl = l.ShortUrl,
            TotalClicks = l.LinkAnalythics.Count
        }).ToArrayAsync();
        return new PagedResult<LinkDto>(links, totalCount);
    }

    public async Task<LinkDto?> UpdateLinkAsync(LinkEditDto dto)
    {
        await using var db = _dbContextFactory.CreateDbContext();
        var link = await db.Links
            .FirstOrDefaultAsync(l=>l.Id == dto.Id && l.UserId == dto.UserId);
        if (link is null)
        {
            return null;
        }
        link.LongUrl = dto.LongUrl;
        link.IsActive = dto.IsActive;
        db.Links.Update(link);
        await db.SaveChangesAsync();
        return new LinkDto
        {
            Id = link.Id,
            LongUrl = link.LongUrl,
            IsActive = link.IsActive,
            ShortUrl = link.ShortUrl,
        };
    }

    public async Task DeleteLinkAsync(long id, string userId)
    {
        await using var db = _dbContextFactory.CreateDbContext();
        var link = db.Links
            .Include(l => l.LinkAnalythics)
            .FirstOrDefault(l => l.Id == id && l.UserId == userId);
        if (link is null)
        {
            return;
        }
        if (link.LinkAnalythics.Count > 0)
            db.LinkAnalythics.RemoveRange(link.LinkAnalythics);
        db.Links.Remove(link);
        await db.SaveChangesAsync();
    }
}