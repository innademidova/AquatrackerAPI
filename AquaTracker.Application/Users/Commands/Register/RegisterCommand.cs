using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<ErrorOr<Success>>;