using App_Link_short.Client.DTOs;

namespace App_Link_short.Client.Interfaces;

public interface ILinkService
{
    Task<LinkDto> GenerateLinkAsync(LinkCreateDto dto);
}