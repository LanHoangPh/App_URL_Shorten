using System.ComponentModel.DataAnnotations;

namespace App_Link_short.Data
{
    public class LinkAnalythic
    {
        [Key]
        public long Id { get; set; }
        public long LinkId { get; set; }
        public DateTime ClickedAt { get; set; }
        public virtual Link? OriginalLink { get; set; }
    }
}
