using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using AquaTracker.Infrastructure.Common.Persistence;

namespace AquaTracker.Infrastructure.Auth.Persistence;

public class AuthRepository: IAuthRepository
{
    private readonly AquaTrackerDbContext _dbContext;

    public AuthRepository(AquaTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Register(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}