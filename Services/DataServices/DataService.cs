namespace DataServices;

public class DataService
{
    private const string _connectionString = "Host=localhost;Database=cheque-writer-ui;Username=postgres;Password=postgres";
    private readonly IDesignTimeDbContextFactory<ChequeWriterDbContext> _dbContextFactory;

    public DataService(IDesignTimeDbContextFactory<ChequeWriterDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Cheque? GetCheque(int id) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from c in db.Cheque.ToList()
            where c.Id == id
            select c;

        return queryResult.FirstOrDefault();
    }
}
