using AquaTracker.Application.Auth.DTOs;
using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Auth.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, ErrorOr<AuthResponse>>
{
    private readonly IJwtTokenService _tokenService;
    private readonly IAppDbContext _dbContext;

    public SignInCommandHandler(IJwtTokenService tokenService, IAppDbContext dbContext)
    {
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return Error.Validation(description:"Email or password are incorrect");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Error.Validation(description:"Email or password are incorrect");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        var response = new AuthResponse(accessToken, refreshToken);
        await _dbContext.CommitChangesAsync();

        return response;
    }
}