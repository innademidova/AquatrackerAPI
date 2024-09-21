using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignOut;

public class SignOutCommandHandler : IRequestHandler<SignOutCommand, ErrorOr<Success>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public SignOutCommandHandler(ICurrentUser currentUser, IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;

        var user = await _usersRepository.GetUserById(userId);
        if (user == null)
        {
            return Error.Failure("User is not authenticated.");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}