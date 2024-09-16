using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands.Register;

public record RegisterCommand(string Name, string Email, string Password)
    : IRequest<ErrorOr<Success>>;