namespace App_Link_short.Client.DTOs;

public class LinkDto
{
    public long Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public bool IsActive { get; set; }
    public int TotalClicks { get; set; }
}