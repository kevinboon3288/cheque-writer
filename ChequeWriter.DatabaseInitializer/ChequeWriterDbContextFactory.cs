using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChequeWriter.DatabaseInitializer
{
    public class ChequeWriterDbContextFactory: IDesignTimeDbContextFactory<ChequeWriterDbContext>
    {
        public ChequeWriterDbContext CreateDbContext(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArgumentException("Empty connection string provided");
            }

            return CreateDbContext(args[0]);
        }

        public static ChequeWriterDbContext CreateDbContext(string connectionString) 
        {
            ArgumentNullException.ThrowIfNull(connectionString);

            DbContextOptionsBuilder<ChequeWriterDbContext> optionBuilder = new();
            optionBuilder.UseNpgsql(connectionString);

            return new ChequeWriterDbContext(optionBuilder.Options);
        }
    }
}
