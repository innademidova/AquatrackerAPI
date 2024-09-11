namespace AquaTracker.Contracts.Users.Requests;

public record RegisterRequest(string FirstName, string LastName, string Email, string Password);