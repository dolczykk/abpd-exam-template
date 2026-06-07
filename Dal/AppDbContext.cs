using Apbd.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apbd.Dal;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Weather> Weathers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Weather>(w =>
        {
            w.HasKey(e => e.Id);
            w.Property(e => e.City).HasMaxLength(40).IsRequired();
            w.Property(e => e.Temperature).IsRequired();
        });
    }
}
