namespace AquaTracker.Domain.Users;
public class User 
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public double DailyWaterGoal { get; set; }
    public ICollection<Water.WaterEntry> WaterEntries { get; set; }
    public string? Gender { get; set; }
    public double? Weight { get; set; }
    public double? ActiveTime { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
