using ChequeWriter.DatabaseInitializer.Models;
using Microsoft.EntityFrameworkCore;

namespace ChequeWriter.DatabaseInitializer;

public class ChequeWriterDbContext : DbContext
{
    public ChequeWriterDbContext(DbContextOptions<ChequeWriterDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cheque>()
            .Property(d => d.DateCreated)
            .HasDefaultValue("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone");
    }

    public DbSet<Cheque> Cheques { get; set; }
}
