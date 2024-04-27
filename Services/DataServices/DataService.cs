namespace DataServices;

public class DataServiceException : Exception
{
    public DataServiceException(string message)
        : base(message)
    {
    }
    public DataServiceException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class DataService : IDataService
{
    private const string _connectionString = "Host=localhost;Database=cheque-writer-ui;Username=postgres;Password=postgres";
    private readonly IDesignTimeDbContextFactory<ChequeWriterDbContext> _dbContextFactory;

    public DataService(IDesignTimeDbContextFactory<ChequeWriterDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    #region ChequeModule

    public Cheque? GetCheques(int id)
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from c in db.Cheque.ToList()
            where c.Id == id
            select c;

        return queryResult.FirstOrDefault();
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
            from ul in db.UserLevel.ToList()
            select ul;

        List<UserLevel>? userLevels = new();

        foreach (UserLevel? userLevel in queryResult)
        {
            userLevels.Add(userLevel);
        }

        return userLevels;
    }

    public bool IsValidUser(string? name, string? password, int userLevel) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from u in db.User.ToList()
            join ul in db.UserLevel on u.UserLevelId equals ul.Id
            where u.Name == name && u.Password == password && u.UserLevelId == userLevel
            select u;

        return queryResult.Any();
    }

    public List<User> GetAllUsers() 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);

        var queryResult =
            from u in db.User
            join ul in db.UserLevel on u.UserLevelId equals ul.Id
            select u;

        return queryResult.ToList();
    }

    public int AddUser(string userName, string password, string? jobTitle, int userLevel) 
    {
        using ChequeWriterDbContext db = _dbContextFactory.CreateDbContext([_connectionString]);
        
        User user = new User() 
        {
            Name = userName,
            Password = password,
            JobTitle = jobTitle,
            UserLevelId = userLevel
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
        if (selectedUser != null) 
        {
            db.Remove(selectedUser);
            result = db.SaveChanges();
        }

        return result;
    }

    #endregion
}
