using DataServices.Models;

namespace DataServices;

public class DataServiceException : Exception
{
    public DataServiceException(string errorMessage): base(errorMessage)
    {
    }
    public DataServiceException(string errorMessage, Exception inner): base(errorMessage, inner)
    {
    }
}

public class DataService : IDataService
{
    private const string DbName = "cheque-writer-ui";
    private string _connectionString;
    private readonly IDesignTimeDbContextFactory<ChequeWriterDbContext> _dbContextFactory;

    public DataService(IDesignTimeDbContextFactory<ChequeWriterDbContext> dbContextFactory, IOptions<ChequeWriterOption> options)
    {
        if (options.Value.ConnectionString == null || !options.Value.ConnectionString.TryGetValue(DbName, out string? connectionString)) 
        {
            throw new DataServiceException($"Unable to retrieve the connection string under {DbName} from configuration");
        }

        _connectionString = connectionString;
        _dbContextFactory = dbContextFactory;
    }

    #region ChequeModule

    public List<Cheque>? GetAllCheques()
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from c in db.Cheque
            select c;

        return queryResult.ToList();
    }

    public Cheque? GetChequeById(int id)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from c in db.Cheque
            where c.Id == id
            select c;

        return queryResult.ToList().FirstOrDefault();
    }

    public int AddCheque(string name, double amount, DateTime? dateCreated, int userId)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        Cheque newCheque = new Cheque()
        {
            Name = name,
            Amount = amount,
            DateCreated = dateCreated,
            UserId = userId
        };

        db.Cheque.Add(newCheque);

        int result = db.SaveChanges();
        if (result == 0)
        {
            throw new DataServiceException("Couldn't add a new user to User table");
        }

        return newCheque.Id;
    }

    public int DeleteCheque(int chequeId)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        Cheque? selectedCheque = db.Cheque.SingleOrDefault(c => c.Id == chequeId);
        if (selectedCheque == null)
        {
            throw new DataServiceException($"Cheque not found, cannot be deleted: {chequeId}");
        }

        db.Cheque.Remove(selectedCheque);
        int result = db.SaveChanges();

        return result;
    }

    #endregion

    #region UserModule

    public string? GetUserLevelNameById(int id) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from ul in db.UserLevel
            where ul.Id == id
            select ul;

        UserLevel? userLevel = queryResult.FirstOrDefault();

        if (userLevel != null) 
        {
            return userLevel.Name;
        }

        return null;
    }

    public List<UserLevel>? GetUserLevels()
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from ul in db.UserLevel
            select ul;

        List<UserLevel>? userLevels = queryResult.ToList();

        return userLevels;
    }

    public User? GetUserByInfo(string? name, int userLevel) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from u in db.User
            join ul in db.UserLevel on u.UserLevelId equals ul.Id
            where u.Name == name && u.UserLevelId == userLevel
            select u;

        return queryResult.ToList().FirstOrDefault();
    }

    public bool IsExistUser(string? name, int userLevel)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from u in db.User
            join ul in db.UserLevel on u.UserLevelId equals ul.Id
            where u.Name == name && u.UserLevelId == userLevel
            select u;

        return queryResult.ToList().Count != 0;
    }

    public List<User> GetAllUsers() 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from u in db.User
            join ul in db.UserLevel on u.UserLevelId equals ul.Id
            orderby u.UserLevelId
            select u;

        return queryResult.ToList();
    }

    public int AddUser(string userName, string password, string? jobTitle, int userLevel, int currentUserId) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        User user = new User()
        {
            UserId = Guid.NewGuid(),
            Name = userName,
            Password = password,
            JobTitle = jobTitle,
            CreatedBy = currentUserId,
            UserLevelId = userLevel,
        };

        db.User.Add(user);
        int result = db.SaveChanges();
        if (result == 0) 
        {
            throw new DataServiceException("Couldn't add a new user to User table");
        }

        return user.Id;
    }

    public int DeleteUser(int userId)
    {
        int result = 0;
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        User? selectedUser = db.User.SingleOrDefault(u => u.Id == userId);
        if (selectedUser == null) 
        {
            throw new DataServiceException($"Couldn't delete the user: {userId}");
        }

        db.Remove(selectedUser);
        result = db.SaveChanges();

        return result;
    }

    #endregion
}
