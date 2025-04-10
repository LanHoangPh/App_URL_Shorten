using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Link_short.Data
{
    public class Link
    {
        [Key]
        public long Id { get; set; }

        [Required, Length(10, 250)]
        public string? LongUrl { get; set; }

        [Required, Length(6, 10)]
        public string? ShortCode { get; set; }
        [Required, Length(10, 50)]
        public string? ShortUrl { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public bool IsActive { get; set; } 
        public virtual ApplicationUser? User { get; set; }

        [InverseProperty(nameof(LinkAnalythic.OriginalLink))]
        public virtual ICollection<LinkAnalythic>? LinkAnalythics { get; set; }
    }
}
