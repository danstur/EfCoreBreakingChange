using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreBreakingChange
{
    public sealed class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(local);Database=TestDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OutboundDeliveryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SiteTypeConfiguration());
        }
    }

    public sealed class OutboundDelivery
    {
        public int Id { get; private set; }

        public Site Sender { get; private set; }
    }

    public sealed class OutboundDeliveryTypeConfiguration : IEntityTypeConfiguration<OutboundDelivery>
    {
        public void Configure(EntityTypeBuilder<OutboundDelivery> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(o => o.Sender)
                .WithMany()
                .HasForeignKey("SenderId")
                .IsRequired();
        }
    }

    public class Site
    {
        public string SiteId { get; private set; }
    }

    public sealed class SiteTypeConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");
            builder.Property(p => p.SiteId)
                .HasColumnName("SapSiteId");
        }
    }

    public static class Program
    {
        private static async Task Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                var deliveries = await ctx.Set<OutboundDelivery>().ToListAsync();
            }
        }
    }
}