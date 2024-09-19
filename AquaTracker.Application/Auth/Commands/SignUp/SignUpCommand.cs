using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignUp;

public record SignUpCommand(string Email, string Password)
    : IRequest<ErrorOr<Success>>;