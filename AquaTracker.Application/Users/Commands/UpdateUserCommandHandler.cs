using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public UpdateUserCommandHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstAsync(u => u.Id == _currentUser.Id, cancellationToken);
        user.Name = request.Name;
        user.Email = request.Email;
        user.Gender = request.Gender;
        user.Weight = request.Weight;
        user.ActiveTime = request.ActiveTime;
        user.DailyWaterGoal = request.DailyWaterGoal;

        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}