using AquaTracker.Application.Common.Interfaces;

namespace AquaTracker.Application.Common.Identity;

internal class CurrentUser : ICurrentUser
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
}