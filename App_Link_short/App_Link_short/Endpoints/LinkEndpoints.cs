using System.Security.Claims;
using App_Link_short.Client.DTOs;
using App_Link_short.Client.Extensions;
using App_Link_short.Client.Interfaces;

namespace App_Link_short.Endpoints;

public static class LinkEndpoints
{
    public static IEndpointRouteBuilder MapLinkEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/links", async (LinkCreateDto dto, ILinkService linkService, ClaimsPrincipal principal) =>
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
        
        
        return app;
    }
    
}