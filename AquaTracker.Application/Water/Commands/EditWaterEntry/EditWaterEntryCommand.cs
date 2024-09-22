using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Commands.EditWaterEntry;

public record EditWaterEntryCommand(double Amount, int Id): IRequest<ErrorOr<Success>>;