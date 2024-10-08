using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Queries.GetMonthlyWaterConsumption;

public record GetMonthlyWaterConsumptionQuery(int Year, int Month): IRequest<ErrorOr<List<WaterEntry>>>;