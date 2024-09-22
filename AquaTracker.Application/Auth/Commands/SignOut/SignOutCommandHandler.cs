using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Auth.Commands.SignOut;

public class SignOutCommandHandler : IRequestHandler<SignOutCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public SignOutCommandHandler(ICurrentUser currentUser, IAppDbContext dbContext)
    {
        _currentUser = currentUser;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);
        if (user == null)
        {
            return Error.Failure("User is not authenticated.");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}