using Microsoft.EntityFrameworkCore;
using Mietify.Backend.Models.DbModels;

namespace Mietify.Backend;

public class MietifyDbContext : DbContext
{
    public virtual DbSet<DbDistrict> Districts { get; set; }
    public virtual DbSet<DbListing> Listings { get; set; }
    public virtual DbSet<DbCity> Cities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbListing>(entity =>
        {
            entity.HasOne<DbDistrict>(e => e.District);
            
        });
        
        modelBuilder.Entity<DbDistrict>(e =>
        {
            e.HasOne<DbCity>(l => l.City);
            e.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(5);
        });  
        
     
    }
}