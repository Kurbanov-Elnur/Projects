
using FluentValidation;
using Project_Work_API.Data.Models.DTO;

namespace Project_Work_API.Validators;

public class SignInUserValidator : AbstractValidator<SignInUserDTO>
{
    public SignInUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .When(x => x.Email != null);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Matches(RegexPatterns.passwordPattern)
            .When(x => x.Password != null);
    }
}