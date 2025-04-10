using App_Link_short.Data;

namespace App_Link_short.Public
{
    public interface ILinkService
    {
        Task<string?> GetLongUrlByShortCodeAsync(string shortCode);
    }
    public class LinkService : ILinkService
    {
        private readonly ApplicationDbContext _context;
        public LinkService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string?> GetLongUrlByShortCodeAsync(string shortCode)
        {
            var link = _context.Links.FirstOrDefault(l=>l.ShortCode == shortCode);
            if (link is null)
            {
                return null;
            }
            var linkAnalytics = new LinkAnalythic
            {
                LinkId = link.Id,
                ClickedAt = DateTime.UtcNow,
            };
            _context.LinkAnalythics.Add(linkAnalytics);
            await _context.SaveChangesAsync();
            return link.LongUrl;
        }
    }
}
