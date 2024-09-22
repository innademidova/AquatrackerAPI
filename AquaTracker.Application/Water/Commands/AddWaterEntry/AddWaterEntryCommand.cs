using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Commands.AddWaterEntry;

public record AddWaterEntryCommand(double Amount): IRequest<ErrorOr<Success>>;