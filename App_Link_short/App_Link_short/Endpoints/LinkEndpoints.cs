using System.Security.Claims;
using App_Link_short.Client.DTOs;
using App_Link_short.Client.Extensions;
using App_Link_short.Client.Interfaces;

namespace App_Link_short.Endpoints;

public static class LinkEndpoints
{
    public static IEndpointRouteBuilder MapLinkEndpoints(this IEndpointRouteBuilder app)
    {
        var linksGroup = app.MapGroup("api/links").RequireAuthorization();
        linksGroup.MapPost("", async (LinkCreateDto dto, ILinkService linkService, ClaimsPrincipal principal) =>
        {
            var userId = principal.GetUserId();
            if (userId != dto.UserId)
            {
                return Results.Unauthorized();
            } 
            var link = await linkService.GenerateLinkAsync(dto);
            return Results.Ok(link);
        }
            ).RequireAuthorization();
        linksGroup.MapGet("", async (int startIndex, int pageSize, bool activeOnly, ILinkService linkService, ClaimsPrincipal principal) =>
        {
            var userId = principal.GetUserId();
            var pageResult = await linkService.GetLinksByUserAsync(userId, startIndex, pageSize, activeOnly);
            Console.WriteLine($"Server: StartIndex={startIndex}, PageSize={pageSize}, Links={pageResult.Record.Length}, TotalCount={pageResult.TotalCount}");
            return Results.Ok(pageResult);
        });
        
        return app;
    }
    
}