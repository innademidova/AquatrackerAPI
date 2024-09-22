using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Queries.GetDailyWaterConsumption;

public record GetDailyWaterConsumptionQuery(): IRequest<ErrorOr<List<WaterEntry>>>;