using AquaTracker.Domain.Users;
using AquaTracker.Domain.Water;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<WaterEntry> Water { get; }
    Task CommitChangesAsync();
}