using App_Link_short.Client.DTOs;

namespace App_Link_short.Client.Interfaces;

public interface ILinkService
{
    Task<LinkDto> GenerateLinkAsync(LinkCreateDto dto);
    Task<PagedResult<LinkDto>> GetLinksByUserAsync(string userId, int strartIndex, int pageSize, bool activeOnly);
}