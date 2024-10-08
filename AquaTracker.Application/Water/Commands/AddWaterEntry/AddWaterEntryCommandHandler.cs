using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Commands.AddWaterEntry;

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
        if (!DateOnly.TryParse(request.Date, out var parsedDate))
        {
            return Error.Failure("Invalid date format. Please use YYYY-MM-DD.");
        }
        
        var waterEntry = new WaterEntry
        {
            Amount = request.Amount,
           Date = parsedDate,
            LoggedTime = TimeOnly.ParseExact(request.Time, "HH:mm"),
            UserId = _currentUser.Id
        };

        await _dbContext.Water.AddAsync(waterEntry, cancellationToken);
        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}