using System.Security.Claims;

namespace App_Link_short.Client.Extensions;

public static class ClaimPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal) => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
}