using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Password)
    : IRequest<ErrorOr<string>>;