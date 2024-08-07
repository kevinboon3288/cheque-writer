﻿namespace DataServices;

public class ChequeWriterDbContext: DbContext
{
    public DbSet<Cheque> Cheque => Set<Cheque>();
    public DbSet<User> User => Set<User>();
    public DbSet<UserLevel> UserLevel => Set<UserLevel>();

    public ChequeWriterDbContext() : base() { }

    public ChequeWriterDbContext(DbContextOptions<ChequeWriterDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
