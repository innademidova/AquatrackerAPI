using AquaTracker.Application.Users.Commands;
using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> GetCurrentUser();
    Task UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken);
    Task<User?> GetUserById(int id);
}