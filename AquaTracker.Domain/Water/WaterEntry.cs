using AquaTracker.Domain.Users;

namespace AquaTracker.Domain.Water;

public class WaterEntry
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly LoggedTime { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}