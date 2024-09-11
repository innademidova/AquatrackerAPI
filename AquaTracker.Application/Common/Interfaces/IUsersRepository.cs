using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task Register(User user);
    Task<User?> GetUserByEmailAsync(string email); 
}