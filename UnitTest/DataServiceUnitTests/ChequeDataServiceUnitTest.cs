namespace DataServiceUnitTests;

[TestFixture]
public class ChequeDataServiceUnitTest
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
        User user1 = new User() { UserId = new Guid("1785E29D-8B5E-49C3-AAC0-E4D2B0E434CC"), Name = "Bob", JobTitle = "Administrator", Password = "bob123", UserLevelId = 1, CreatedBy = 0, UserLevel = adminLevel };
        User user2 = new User() { UserId = new Guid("9BCEAB40-7F2B-459B-A99A-9F93F92EBAED"), Name = "Anthony", JobTitle = "Engineer", Password = "ant234", UserLevelId = 2, CreatedBy = 1, UserLevel = userLevel };
        Cheque defaultCheque = new Cheque(){ Name = "Tg Company", Amount = 12778.34, DateCreated = new DateTime(2011, 6, 5), UserId = user1.Id, User = user1 };

        using (ChequeWriterDbContext dbContext = new(_dbContextOptions))
        {
            dbContext.Database.EnsureCreated();

            dbContext.UserLevel.Add(adminLevel);
            dbContext.UserLevel.Add(userLevel);
            dbContext.User.Add(user1);
            dbContext.User.Add(user2);
            dbContext.Cheque.Add(defaultCheque);

            dbContext.SaveChanges();
        }
    }

    [TearDown]
    public void TearDown()
    {
        _dbConnection.Dispose();
    }

    [Test]
    public void GetAllCheques_ReturnAllCheques_HasCheques()
    {
        // Act
        DateTime dateTime = new DateTime(2011, 6, 5);

        List<Cheque>? cheques = _dataService.GetAllCheques();

        // Assert
        Assert.That(cheques, Is.Not.Null);
        Assert.That(cheques.Count(), Is.EqualTo(1));
        Assert.That(cheques[0].Id, Is.EqualTo(1));
        Assert.That(cheques[0].Name, Is.EqualTo("Tg Company"));
        Assert.That(cheques[0].Amount, Is.EqualTo(12778.34));
        Assert.That(cheques[0].DateCreated, Is.EqualTo(dateTime));
    }

    [Test]
    public void GetAllCheques_ReturnEmptyCheques_HasEmptyCheques()
    {
        // Arrange
        using (ChequeWriterDbContext dbContext = new(_dbContextOptions))
        {
            dbContext.Database.EnsureCreated();
            dbContext.Cheque.RemoveRange(dbContext.Cheque);
            dbContext.SaveChanges();
        }

        // Act
        List<Cheque>? cheques = _dataService.GetAllCheques();

        // Assert
        Assert.That(cheques, Is.Not.Null);
        Assert.That(cheques, Is.Empty);
        Assert.That(cheques.Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddCheque_ReturnNewChequeId_WithNewChequeInfo()
    {
        // Act
        int newChequeId = _dataService.AddCheque("E&E Company", 178.45, DateTime.Now, 1);

        bool isNewUserAdded = _dataService.GetAllCheques().Any(x => x.Id == newChequeId);

        // Assert
        Assert.That(newChequeId, Is.EqualTo(2));
        Assert.That(isNewUserAdded, Is.True);
    }

    [Test]
    public void DeleteCheque_ReturnTrue_WithExistCheque()
    {
        // Act
        _dataService.DeleteUser(1);

        bool isDeletedUserFound = _dataService.GetAllCheques().Any(x => x.Id == 1);

        // Assert
        Assert.That(isDeletedUserFound, Is.False);
    }

    [Test]
    public void DeleteCheque_ThrowDataServiceException_WithInvalidChequeId()
    {
        // Assert
        Assert.Throws<DataServiceException>(() => _dataService.DeleteCheque(4));
    }
}