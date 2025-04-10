namespace App_Link_short.Client.DTOs
{
    public class LinkAnalyticsDto
    {
        public long Id { get; set; }
        public long LinkId { get; set; }
        public DateTime ClickedAt { get; set; }
    }
}
