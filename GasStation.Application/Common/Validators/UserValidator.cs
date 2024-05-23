using GasStation.Domain.Entities;
using FluentValidation;


namespace GasStation.Application.Common.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Fullname must not be empty")
            .Length(3, 50)
            .WithMessage("Fullname must be between 3 and 50 characters");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email must not be empty")
            .Length(3, 50)
            .EmailAddress()
            .WithMessage("Email must be between 3 and 50 characters");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password must not be empty")
            .Length(6, 16)
            .WithMessage("Password must be between 3 and 50 characters");
    }
}
