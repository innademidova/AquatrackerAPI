using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IAuthRepository
{
    Task Register(User user);
}