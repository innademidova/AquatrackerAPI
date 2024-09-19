using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using AquaTracker.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Infrastructure.Users.Persistence;

public class UsersRepository: IUsersRepository
{
    private readonly AquaTrackerDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public UsersRepository(AquaTrackerDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> GetCurrentUser()
    {
        var user = await _dbContext.Users.FirstAsync(u => u.Id == _currentUser.Id);

        return user;
    }
}