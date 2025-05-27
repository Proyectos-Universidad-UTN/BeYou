using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class TaxValidator : AbstractValidator<Tax>
{
    public TaxValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(40).WithMessage("El nombre no puede tener mas de 40 caracteres");

        RuleFor(x => x.Rate)
            .NotEmpty().WithMessage("Por favor ingrese la tasa")
            .InclusiveBetween(0, 100).WithMessage("La tasa debe estar entre 0 y 100");
    }
}
