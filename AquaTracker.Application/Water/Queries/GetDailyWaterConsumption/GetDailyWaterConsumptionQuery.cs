using AquaTracker.Domain.Water;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Queries.GetDailyWaterConsumption;

public record GetDailyWaterConsumptionQuery(string Date): IRequest<ErrorOr<List<WaterEntry>>>;