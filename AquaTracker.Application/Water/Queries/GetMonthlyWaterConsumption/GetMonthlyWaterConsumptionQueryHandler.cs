using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Water.Queries.GetMonthlyWaterConsumption;

public class GetMonthlyWaterConsumptionQueryHandler: IRequestHandler<GetMonthlyWaterConsumptionQuery, ErrorOr<List<WaterEntry>>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetMonthlyWaterConsumptionQueryHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }
    public async Task<ErrorOr<List<WaterEntry>>> Handle(GetMonthlyWaterConsumptionQuery request, CancellationToken cancellationToken)
    {
        var firstDayOfMonth = DateOnly.FromDateTime(new DateTime(request.Year, request.Month, 1));
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        
        var waterEntries = await _dbContext.Water
            .Where(w => w.UserId == _currentUser.Id && w.Date >= firstDayOfMonth && w.Date <= lastDayOfMonth)
            .ToListAsync(cancellationToken);

        return waterEntries;
    }
}