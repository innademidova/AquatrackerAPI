using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Application.Water.Commands.AddWaterEntry;
using ErrorOr;
using MediatR;
using AquaTracker.Domain.Water;

namespace AquaTracker.Application.Water.Commands;

public class AddWaterEntryCommandHandler : IRequestHandler<AddWaterEntryCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public AddWaterEntryCommandHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<Success>> Handle(AddWaterEntryCommand request, CancellationToken cancellationToken)
    {
        var waterEntry = new WaterEntry
        {
            Amount = request.Amount,
            Time = DateTime.UtcNow,
            UserId = _currentUser.Id
        };

        await _dbContext.Water.AddAsync(waterEntry, cancellationToken);
        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}