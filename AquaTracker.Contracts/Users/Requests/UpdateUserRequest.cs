namespace AquaTracker.Contracts.Users.Requests;

public record UpdateUserRequest(
    string Name,
    string Email,
    string Gender,
    double DailyWaterGoal,
    double Weight,
    double ActiveTime);