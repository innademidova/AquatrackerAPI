using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Water.Commands.DeleteWaterEntry;

public record DeleteWaterEntryCommand(int Id) : IRequest<ErrorOr<Success>>;