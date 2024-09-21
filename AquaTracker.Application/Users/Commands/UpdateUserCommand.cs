using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands;

public record UpdateUserCommand(
    string Name,
    string Email,
    string Gender,
    double DailyWaterGoal,
    double Weight,
    double ActiveTime): IRequest<ErrorOr<Success>>;