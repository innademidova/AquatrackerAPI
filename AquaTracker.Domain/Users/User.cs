namespace AquaTracker.Domain.Users;

public class User 
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public double DailyWaterGoal { get; set; }
    public ICollection<Water.Water> WaterEntries { get; set; } // Связь с записями о выпитой воде
}