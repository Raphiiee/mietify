using Microsoft.EntityFrameworkCore;
using Mietify.Backend.Models.DbModels;

namespace Mietify.Backend;

public class MietifyDbContext : DbContext
{
    public virtual DbSet<DbDistrict> Districts { get; set; }
    public virtual DbSet<DbListing> Listings { get; set; }

    public MietifyDbContext(DbContextOptions<MietifyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbListing>(entity =>
        {
            entity.HasOne<DbDistrict>(e => e.District)
                .WithMany(e => e.Listings)
                .IsRequired();
        });

        modelBuilder.Entity<DbDistrict>(e =>
        {
            e.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(5);
        });
    }
}