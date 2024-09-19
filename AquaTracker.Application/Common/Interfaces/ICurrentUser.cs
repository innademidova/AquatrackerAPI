namespace AquaTracker.Application.Common.Interfaces;

public interface ICurrentUser
{
    int Id { get; set; }
    string Email { get; set; }
}