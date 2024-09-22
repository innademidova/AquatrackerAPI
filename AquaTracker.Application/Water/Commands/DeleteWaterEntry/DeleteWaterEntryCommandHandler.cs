using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Water.Commands.DeleteWaterEntry;

public class DeleteWaterEntryCommandHandler : IRequestHandler<DeleteWaterEntryCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;

    public DeleteWaterEntryCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(DeleteWaterEntryCommand request, CancellationToken cancellationToken)
    {
        var waterEntry = await _dbContext.Water.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (waterEntry == null)
        {
            return Error.Failure("Water entry does not exist.");
        }

        _dbContext.Water.Remove(waterEntry);
        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}