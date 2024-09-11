using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using AquaTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Infrastructure.Users.Persistence;

public class UsersRepository: IUsersRepository
{
    private readonly AquaTrackerDbContext _dbContext;

    public UsersRepository(AquaTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Register(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }
}