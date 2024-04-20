namespace DataServices;

public class ChequeWriterDbContextFactory : IDesignTimeDbContextFactory<ChequeWriterDbContext>
{
    public ChequeWriterDbContext CreateDbContext(string[] args)
    {
        string? connectionString = (args.Length != 0) ? args[0] : throw new ArgumentNullException(nameof(args));

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string is required.");
        }

        DbContextOptionsBuilder<ChequeWriterDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new ChequeWriterDbContext(optionsBuilder.Options);
    }
}