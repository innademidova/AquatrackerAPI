using AquaTracker.Domain.Users;

namespace AquaTracker.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
