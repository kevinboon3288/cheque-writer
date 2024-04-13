namespace DataServices;

public class ChequeWriterDbContextFactory : IDesignTimeDbContextFactory<ChequeWriterDbContext>
{
    public ChequeWriterDbContext CreateDbContext(string[] args)
    {
        //TODO: Temporary get connectionString from args or config file for Add-Migration. Find a better way to resolve this more dynamically.
        string? connectionString = (args.Length != 0) ? args[0] : ConfigurationManager.ConnectionStrings["cheque-writer-ui"].ConnectionString;

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string is required.");
        }

        DbContextOptionsBuilder<ChequeWriterDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        return new ChequeWriterDbContext(optionsBuilder.Options);
    }
}