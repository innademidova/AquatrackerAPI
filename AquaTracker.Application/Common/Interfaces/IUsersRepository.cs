using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> GetCurrentUser();
}