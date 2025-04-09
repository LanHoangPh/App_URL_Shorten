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
}