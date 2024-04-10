namespace ChequeWriter.DbInitializer;

public static class DbInitializer
{
    public static void Seed(string connectionString) 
    {
        ArgumentNullException.ThrowIfNull(nameof(connectionString));

        DbContextOptionsBuilder<ChequeWriterDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(connectionString);

        using ChequeWriterDbContext db = new ChequeWriterDbContext(optionsBuilder.Options);

        db.Database.EnsureCreated();

        ClearAllData(db);
        AddUserLevel(db);
        db.SaveChanges();
    }

    private static void ClearAllData(ChequeWriterDbContext db) 
    {
        db.Cheque.RemoveRange(db.Cheque);
        db.User.RemoveRange(db.User);
        db.UserLevel.RemoveRange(db.UserLevel);
    }

    private static void AddUserLevel(ChequeWriterDbContext db) 
    {
        db.UserLevel.Add(new DataServices.Models.UserLevel() { Name = "User" });
        db.UserLevel.Add(new DataServices.Models.UserLevel() { Name = "Admin" });
    }
}
