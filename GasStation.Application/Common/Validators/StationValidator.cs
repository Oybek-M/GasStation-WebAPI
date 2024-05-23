using FluentValidation;
using GasStation.Domain.Entities;

namespace GasStation.Application.Common.Validators;

public class StationValidator : AbstractValidator<Station>
{
    public StationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty")
            .Length(3, 50)
            .WithMessage("Name must be between 3 and 50 characters");
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price must not be empty");
        RuleFor(x => x.StationType)
            .NotEmpty()
            .WithMessage("Station type must not be empty");
    }
}
