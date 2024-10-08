using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignOut;

public record SignOutCommand(): IRequest<ErrorOr<Success>>;