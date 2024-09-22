using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Water.Queries.GetDailyWaterConsumption;

public class
    GetDailyWaterConsumptionQueryHandler : IRequestHandler<GetDailyWaterConsumptionQuery, ErrorOr<List<WaterEntry>>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetDailyWaterConsumptionQueryHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<List<WaterEntry>>> Handle(GetDailyWaterConsumptionQuery request,
        CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        var dailyWaterConsumption = await _dbContext.Water.Where(w => w.UserId == _currentUser.Id && w.Time >= today && w.Time < tomorrow)
            .ToListAsync(cancellationToken);

        return dailyWaterConsumption;
    }
}