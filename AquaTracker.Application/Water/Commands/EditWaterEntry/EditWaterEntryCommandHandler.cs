using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Water.Commands.EditWaterEntry;

public class EditWaterEntryCommandHandler : IRequestHandler<EditWaterEntryCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;

    public EditWaterEntryCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(EditWaterEntryCommand request, CancellationToken cancellationToken)
    {
        var waterEntry = await _dbContext.Water.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (waterEntry == null)
        {
            return Error.Failure("Water entry does not exist.");
        }

        waterEntry.Amount = request.Amount;

        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}