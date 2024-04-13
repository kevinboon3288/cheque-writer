namespace DataServices;

public class ChequeWriterDbContextFactory : IDesignTimeDbContextFactory<ChequeWriterDbContext>
{
    public ChequeWriterDbContext CreateDbContext(string[] args)
    {
        if (args.Length == 0 || args is null) 
        { 
            throw new ArgumentNullException(nameof(args));
        }

        string connectionString = args[0];

        ArgumentNullException.ThrowIfNull(connectionString);

        DbContextOptionsBuilder<ChequeWriterDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new ChequeWriterDbContext(optionsBuilder.Options);
    }
}
