using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
