namespace AquaTracker.Application.Users.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public double DailyWaterGoal { get; set; }
    public string? Gender { get; set; }
    public double? Weight { get; set; }
    public double? ActiveTime { get; set; }
}