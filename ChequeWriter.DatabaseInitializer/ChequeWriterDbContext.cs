namespace ChequeWriter.DatabaseInitializer
{
    public class ChequeWriterDbContext: DbContext
    {
        public ChequeWriterDbContext() : base() { }

        public ChequeWriterDbContext(DbContextOptions<ChequeWriterDbContext> options) : base(options) { }

        public DbSet<Cheque> Cheque { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cheque"].ConnectionString;

            ArgumentNullException.ThrowIfNull(connectionString);

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
