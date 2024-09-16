namespace AquaTracker.Contracts.Users.Requests;

public record RegisterRequest(string Name, string Email, string Password);