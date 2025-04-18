using App_Link_short.Client.DTOs;
using Refit;

namespace App_Link_short.Client.Services;

public interface ILinkApi
{
    [Post("/api/link")]
    Task<LinkDto> CreateLinkAsync(LinkCreateDto dto);
    [Get("/api/links")]
    Task<PagedResult<LinkDto>> GetLinksByUserAsync([Query]int strartIndex, int pageSize, bool activeOnly);
    [Patch("/api/link/{linkId}")]
    Task<LinkDto?> UpdateLinkAsync(long linkId, LinkEditDto dto);
    [Delete("/api/link/{linkId}")]
    Task DeleteLinkAsync(long linkId);
    [Get("/api/link/{linkId}")]
    Task<LinkDetailsDto?> GetLinkAsync(long linkId);


}