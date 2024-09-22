using System.Reflection;
using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Infrastructure.Common.Persistence;

public class AquaTrackerDbContext: DbContext, IUnitOfWork, IAppDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Domain.Water.Water> Water { get; set; } = null!;

    public AquaTrackerDbContext(DbContextOptions options): base(options)
    {
    }
    
    public async Task CommitChangesAsync()  
    {
        await SaveChangesAsync();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}