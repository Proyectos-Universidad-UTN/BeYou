using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener mas de 80 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("El nombre no puede tener mas de 150 caracteres");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Por favor ingrese el teléfono");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico")
            .EmailAddress().WithMessage("Por favor ingrese una dirección de correo electrónico válido");

        RuleFor(x => x.DistrictId)
            .NotEmpty().WithMessage("Por favor ingrese un distrito válido");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Por favor ingrese un dirección válida")
            .When(x => x.Address != null);
    }
}
