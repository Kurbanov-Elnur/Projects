using Auth.Shared.DTOs;
using FluentValidation;

namespace Auth.Application.Validators;

public class SignUpValidator : AbstractValidator<SignUpDto>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Matches(RegexPatterns.NameSurnamePattern);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Surname is required")
            .Matches(RegexPatterns.NameSurnamePattern);

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .Matches(RegexPatterns.UsernamePattern);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Valid Email is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Matches(RegexPatterns.PasswordPattern)
            .NotEqual(x => x.Username);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm Password is required")
            .Equal(x => x.Password)
            .WithMessage("Password and Confirm Password must match");
    }
}