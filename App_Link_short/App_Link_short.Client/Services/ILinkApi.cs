using App_Link_short.Client.DTOs;
using Refit;

namespace App_Link_short.Client.Services;

public interface ILinkApi
{
    [Post("/api/link")]
    Task<LinkDto> CreateLinkAsync(LinkCreateDto dto);
    [Get("/api/links")]
    Task<PagedResult<LinkDto>> GetLinksByUserAsync([Query]int strartIndex, int pageSize, bool activeOnly);
}