using AquaTracker.Application.Auth.DTOs;
using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, ErrorOr<AuthResponse>>
{
    private readonly IJwtTokenService _tokenService;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SignInCommandHandler(IJwtTokenService tokenService, IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            return Error.Failure("Email or password are incorrect");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Error.Failure("Email or password are incorrect");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        var response = new AuthResponse(accessToken, refreshToken);
        await _unitOfWork.CommitChangesAsync();

        return response;
    }
}