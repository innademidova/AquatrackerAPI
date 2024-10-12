using AquaTracker.Application.Auth.DTOs;
using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, ErrorOr<AuthResponse>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IJwtTokenService _tokenService;
    private readonly ICurrentUser _currentUser;

    public RefreshTokenCommandHandler(IJwtTokenService tokenService, ICurrentUser currentUser, IAppDbContext dbContext)
    {
        _tokenService = tokenService;
        _currentUser = currentUser;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);
        if (user == null || user.RefreshToken != request.RefreshToken)
        {
            return Error.Validation(description: "Invalid refresh token.");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _dbContext.CommitChangesAsync();
        
        var response = new AuthResponse(newAccessToken, newRefreshToken);
        return response;
    }
}