using Auth.Shared.DTOs;
using FluentValidation;

namespace Auth.Application.Validators;

public class SignInValidator: AbstractValidator<SignInDto>
{
    public SignInValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .Matches(RegexPatterns.UsernamePattern)
            .When(x => x.Username != null);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Matches(RegexPatterns.PasswordPattern)
            .When(x => x.Password != null);
    }
}