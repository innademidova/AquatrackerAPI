using AquaTracker.Application.Users.Commands.Register;
using FluentValidation;

namespace AquaTracker.Application.Users.Commands.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(300);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(300);
        RuleFor(e => e.Password).NotEmpty().MinimumLength(8);
    }
}