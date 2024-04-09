namespace DataServices;

public class ChequeWriterDbContext: DbContext
{
    public DbSet<Cheque> Cheque => Set<Cheque>();

    public ChequeWriterDbContext() : base() { }

    public ChequeWriterDbContext(DbContextOptions<ChequeWriterDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
