using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Application.Users.Commands;
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

    public async Task UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstAsync(u => u.Id == _currentUser.Id, cancellationToken);
        user.Name = command.Name;
        user.Email = command.Email;
        user.Gender = command.Gender;
        user.Weight = command.Weight;
        user.ActiveTime = command.ActiveTime;
        user.DailyWaterGoal = command.DailyWaterGoal;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
}