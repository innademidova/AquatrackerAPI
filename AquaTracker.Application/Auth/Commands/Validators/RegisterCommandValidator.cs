using AquaTracker.Application.Auth.Commands.SignUp;
using FluentValidation;

namespace AquaTracker.Application.Auth.Commands.Validators;

public class RegisterCommandValidator : AbstractValidator<SignUpCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(e => e.Password).NotEmpty().MinimumLength(8) ;
    }
}