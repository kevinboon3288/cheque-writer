namespace DataServiceUnitTests;

[TestFixture]
public class UserDataServiceUnitTest
{
    private const string DbConnectionString = "Filename=:memory:";
    private IDataService _dataService;
    private IDesignTimeDbContextFactory<ChequeWriterDbContext> _mockDbContextFactory;
    private DbConnection _dbConnection;
    private DbContextOptions<ChequeWriterDbContext> _dbContextOptions;

    [SetUp]
    public void SetUp()
    {
        _dbConnection = new SqliteConnection(DbConnectionString);
        _dbConnection.Open();

        _dbContextOptions = new DbContextOptionsBuilder<ChequeWriterDbContext>()
            .UseSqlite(_dbConnection)
            .Options;

        _mockDbContextFactory = Substitute.For<IDesignTimeDbContextFactory<ChequeWriterDbContext>>();
        _mockDbContextFactory.CreateDbContext(Arg.Is<string[]>(s => s[0] == DbConnectionString)).Returns(x =>
        {
            ChequeWriterDbContext dbContext = new(_dbContextOptions);
            dbContext.Database.EnsureCreated();
            return dbContext;
        });

        _dataService = new DataService(_mockDbContextFactory, Options.Create(new ChequeWriterOption
        {
            ConnectionString = new Dictionary<string, string>() { { "cheque-writer-ui", DbConnectionString } }
        })) ;

        UserLevel adminLevel = new UserLevel() { Name = "Admin" };
        UserLevel userLevel = new UserLevel() { Name = "User" };

        using (ChequeWriterDbContext dbContext = new(_dbContextOptions))
        {
            dbContext.Database.EnsureCreated();

            dbContext.UserLevel.Add(adminLevel);
            dbContext.UserLevel.Add(userLevel);
            dbContext.User.Add(new User() { Name = "Bob", JobTitle = "Administrator", Password = "bob123", UserLevelId = 1, UserLevel = adminLevel });
            dbContext.User.Add(new User() { Name = "Anthony", JobTitle = "Engineer", Password = "ant234", UserLevelId = 2, UserLevel = userLevel });
            dbContext.User.Add(new User() { Name = "Jessie Tan", JobTitle = "Developer", Password = "j@dev555", UserLevelId = 2, UserLevel = userLevel });

            dbContext.SaveChanges();
        }
    }

    [TearDown]
    public void TearDown()
    {
        _dbConnection.Dispose();
    }

    [Test]
    public void GetUserLevelNameById_ReturnString_Success()
    {
        // Act
        string? userLevel1Name = _dataService.GetUserLevelNameById(1);
        string? userLevel2Name = _dataService.GetUserLevelNameById(2);

        // Assert
        Assert.That(userLevel1Name, Is.Not.Null);
        Assert.That(userLevel2Name, Is.Not.Null);
        Assert.That(userLevel1Name, Is.EqualTo("Admin"));
        Assert.That(userLevel2Name, Is.EqualTo("User"));
    }

    [Test]
    public void GetUserLevelNameById_ReturnNull_Success()
    {
        // Act
        string? userLevelName = _dataService.GetUserLevelNameById(3);

        // Assert
        Assert.That(userLevelName, Is.Null);
    }

    [Test]
    public void GetUserLevels_ReturnAllUserLevel_Success()
    {
        // Act
        List<UserLevel>? userLevels = _dataService.GetUserLevels();

        // Assert
        Assert.That(userLevels, Is.Not.Null);
        Assert.That(userLevels.Count(), Is.EqualTo(2));
        Assert.That(userLevels[0].Id, Is.EqualTo(1));
        Assert.That(userLevels[0].Name, Is.EqualTo("Admin"));
        Assert.That(userLevels[1].Id, Is.EqualTo(2));
        Assert.That(userLevels[1].Name, Is.EqualTo("User"));
    }

    [Test]
    public void GetUserLevels_ReturnNull_Success()
    {
        // Arrange
        using (ChequeWriterDbContext dbContext = new(_dbContextOptions))
        {
            dbContext.Database.EnsureCreated();
            dbContext.UserLevel.RemoveRange(dbContext.UserLevel);
            dbContext.SaveChanges();
        }

        // Act
        List<UserLevel>? userLevels = _dataService.GetUserLevels();

        // Assert
        Assert.That(userLevels, Is.Not.Null);
        Assert.That(userLevels, Is.Empty);
        Assert.That(userLevels.Count(), Is.EqualTo(0));
    }

    [Test]
    public void IsValidUser_ReturnTrue_Success()
    {
        // Act
        bool result = _dataService.IsValidUser("Bob", "bob123", 1);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidUser_ReturnFalse_Success()
    {
        // Act
        bool result = _dataService.IsValidUser("Kevin", "admin123", 1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsExistUser_ReturnTrue_Success()
    {
        // Act
        bool result = _dataService.IsExistUser("Bob", 1);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsExistUser_ReturnFalse_Success()
    {
        // Act
        bool result = _dataService.IsExistUser("Kevin", 1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetAllUsers_ReturnAllUsers_Success()
    {
        // Act
        List<User> users = _dataService.GetAllUsers();

        // Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.EqualTo(3));
        Assert.That(users[0].Id, Is.EqualTo(1));
        Assert.That(users[0].Name, Is.EqualTo("Bob"));
        Assert.That(users[0].JobTitle, Is.EqualTo("Administrator"));
        Assert.That(users[0].Password, Is.EqualTo("bob123"));
        Assert.That(users[0].UserLevelId, Is.EqualTo(1));
        Assert.That(users[1].Id, Is.EqualTo(2));
        Assert.That(users[1].Name, Is.EqualTo("Anthony"));
        Assert.That(users[1].JobTitle, Is.EqualTo("Engineer"));
        Assert.That(users[1].Password, Is.EqualTo("ant234"));
        Assert.That(users[1].UserLevelId, Is.EqualTo(2));
        Assert.That(users[2].Id, Is.EqualTo(3));
        Assert.That(users[2].Name, Is.EqualTo("Jessie Tan"));
        Assert.That(users[2].JobTitle, Is.EqualTo("Developer"));
        Assert.That(users[2].Password, Is.EqualTo("j@dev555"));
        Assert.That(users[2].UserLevelId, Is.EqualTo(2));
    }

    [Test]
    public void GetAllUsers_ReturnAllUsers_Null()
    {
        // Arrange
        using (ChequeWriterDbContext dbContext = new(_dbContextOptions))
        {
            dbContext.Database.EnsureCreated();
            dbContext.User.RemoveRange(dbContext.User);
            dbContext.SaveChanges();
        }

        // Act
        List<User>? users = _dataService.GetAllUsers();

        // Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users, Is.Empty);
        Assert.That(users.Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddUser_ReturnUserId_Success()
    {
        // Act
        string newUserName = "Nancy";
        string newUserPassword = "nancy@112";
        string newUserJobTitle = "Tester";

        int newUserId = _dataService.AddUser(newUserName, newUserPassword, newUserJobTitle, 2);

        bool isNewUserAdded = _dataService.GetAllUsers().Any(x => x.Name == newUserName 
                                && x.Password == newUserPassword
                                && x.JobTitle == newUserJobTitle);

        // Assert
        Assert.That(newUserId, Is.EqualTo(4));
        Assert.That(isNewUserAdded, Is.True);
    }

    [Test]
    public void DeleteUser_ReturnResultValue_Success()
    {
        // Act
        _dataService.DeleteUser(3);

        bool isDeletedUserFound = _dataService.GetAllUsers().Any(x => x.Id == 3);

        // Assert
        Assert.That(isDeletedUserFound, Is.False);
    }

    [Test]
    public void DeleteUser_UnknownId_ThrowDataServiceException()
    {
        // Assert
        Assert.Throws<DataServiceException>(() => _dataService.DeleteUser(4));
    }
}