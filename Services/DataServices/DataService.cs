namespace DataServices;

public class DataService : IDataService
{
    private const string _connectionString = "Host=localhost;Database=cheque-writer-ui;Username=postgres;Password=postgres";
    private readonly IDesignTimeDbContextFactory<ChequeWriterDbContext> _dbContextFactory;

    public DataService(IDesignTimeDbContextFactory<ChequeWriterDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Cheque? GetCheques(int id)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from c in db.Cheque.ToList()
            where c.Id == id
            select c;

        return queryResult.FirstOrDefault();
    }

    public List<UserLevel>? GetUserLevels()
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from ul in db.UserLevel.ToList()
            select ul;

        List<UserLevel>? userLevels = new();

        foreach (UserLevel? userLevel in queryResult)
        {
            userLevels.Add(userLevel);
        }

        return userLevels;
    }
}
