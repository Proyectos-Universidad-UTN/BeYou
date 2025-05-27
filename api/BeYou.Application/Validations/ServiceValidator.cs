using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class ServiceValidator : AbstractValidator<Service>
{
    public ServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener mas de 80 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("El nombre no puede tener mas de 150 caracteres");

        RuleFor(x => x.Observation)
            .NotEmpty().WithMessage("Por favor ingrese la observación")
            .MaximumLength(150).WithMessage("La observación no puede tener mas de 150 caracteres");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico");

        RuleFor(x => x.TypeServiceId)
            .NotEmpty().WithMessage("Por favor ingrese un Tipo de servicio válido");

    }
}
