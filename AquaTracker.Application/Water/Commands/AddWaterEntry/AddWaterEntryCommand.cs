using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Commands.AddWaterEntry;

public record AddWaterEntryCommand(double Amount, string Date, string Time): IRequest<ErrorOr<Success>>;