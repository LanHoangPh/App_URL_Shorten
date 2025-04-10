using App_Link_short.Client.DTOs;
using App_Link_short.Client.Interfaces;

namespace App_Link_short.Client.Services;

public class LinkApiProxy : ILinkService
{
    private readonly ILinkApi _linkApi;

    public LinkApiProxy(ILinkApi linkApi)
    {
        _linkApi = linkApi;
    }

    public Task<LinkDto> GenerateLinkAsync(LinkCreateDto dto) => _linkApi.CreateLinkAsync(dto);

    public Task<PagedResult<LinkDto>>
        GetLinksByUserAsync(string userId, int strartIndex, int pageSize, bool activeOnly) =>
        _linkApi.GetLinksByUserAsync(strartIndex, pageSize, activeOnly);

    public Task<LinkDto?> UpdateLinkAsync(LinkEditDto dto) => _linkApi.UpdateLinkAsync(dto.Id, dto);
    public Task DeleteLinkAsync(long id, string userId) => _linkApi.DeleteLinkAsync(id);    
}