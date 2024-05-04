namespace ChequeWriter.DbInitializer;

public static class DbInitializer
{
    public static void Seed(string connectionString) 
    {
        ArgumentNullException.ThrowIfNull(nameof(connectionString));

        ChequeWriterDbContextFactory dbContextFactory = new ChequeWriterDbContextFactory();
        ChequeWriterDbContext db = dbContextFactory.CreateDbContext([connectionString]);
        db.Database.EnsureCreated();

        ClearAllData(db);
        AddUserLevel(db);
        AddDefaultAdminUser(db);
    }

    private static void ClearAllData(ChequeWriterDbContext db) 
    {
        db.Cheque.RemoveRange(db.Cheque);
        db.User.RemoveRange(db.User);
        db.UserLevel.RemoveRange(db.UserLevel);
    }

    private static void AddDefaultAdminUser(ChequeWriterDbContext db) 
    {
        DataServices.Models.UserLevel? userLevel = db.UserLevel.FirstOrDefault(x => x.Name == "Admin");
        db.User.Add(new DataServices.Models.User() { Name = "Admin", UserId = Guid.NewGuid(), Password="admin123", JobTitle = "Admin", CreatedBy = 0, UserLevel = userLevel });
        db.SaveChanges();
    }

    private static void AddUserLevel(ChequeWriterDbContext db) 
    {
        db.UserLevel.Add(new DataServices.Models.UserLevel() { Name = "Admin" });
        db.UserLevel.Add(new DataServices.Models.UserLevel() { Name = "User" });
        db.SaveChanges();
    }
}
