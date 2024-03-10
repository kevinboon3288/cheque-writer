using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace ChequeWriter.DatabaseInitializer
{
    public class ChequeWriterDatabaseInitializer
    {
        private const string _connectionName = "cheque-writer-ui";
        private readonly string _connectionString;

        private readonly IDesignTimeDbContextFactory<ChequeWriterDbContext> _dbContextFactory;

        public ChequeWriterDatabaseInitializer(IDesignTimeDbContextFactory<ChequeWriterDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _connectionString = ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;

            using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext(new string[] { _connectionString });
            string currentMigration = db.Database.GetAppliedMigrations().LastOrDefault();
        }
    }
}
