using AquaTracker.Domain.Users;

namespace AquaTracker.Domain.Water;

public class Water
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public DateTime Time{ get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}