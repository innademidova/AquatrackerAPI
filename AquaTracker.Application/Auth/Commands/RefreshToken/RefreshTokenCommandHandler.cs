using AquaTracker.Application.Auth.DTOs;
using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, ErrorOr<AuthResponse>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenService _tokenService;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IUsersRepository usersRepository, IJwtTokenService tokenService, ICurrentUser currentUser, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _tokenService = tokenService;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        
        var user = await _usersRepository.GetUserById(userId);
        if (user == null || user.RefreshToken != request.RefreshToken)
        {
            return Error.Failure("Invalid refresh token.");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _unitOfWork.CommitChangesAsync();
        
        var response = new AuthResponse(newAccessToken, newRefreshToken);
        return response;
    }
}