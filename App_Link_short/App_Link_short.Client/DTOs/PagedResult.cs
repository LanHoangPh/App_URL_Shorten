namespace App_Link_short.Client.DTOs;

public record class PagedResult<TResult>(TResult[] Record, int TotalCount);