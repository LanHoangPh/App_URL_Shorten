namespace App_Link_short.Client.DTOs;

public record LinkEditDto(long Id, string LongUrl, bool IsActive, string UserId);