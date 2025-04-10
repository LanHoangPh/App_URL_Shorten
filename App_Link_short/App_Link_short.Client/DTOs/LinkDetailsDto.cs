namespace App_Link_short.Client.DTOs
{
    public record class LinkDetailsDto(LinkDto Link, LinkAnalyticsDto[] LinkAnalytics);
}
