using Microsoft.EntityFrameworkCore;

namespace App_Link_short.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Link> Links { get; set; } = null!;
        public DbSet<LinkAnalythic> LinkAnalythics { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Link>()
               .HasIndex(l => l.ShortCode)
                .IsUnique();
            //builder.Entity<LinkAnalythic>()
            //    .HasOne(la => la.OriginalLink)
            //    .WithMany(l => l.LinkAnalythics)
            //    .HasForeignKey(la => la.LinkId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
