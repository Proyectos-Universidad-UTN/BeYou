using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class VendorValidator : AbstractValidator<Vendor>
{
    public VendorValidator()
    {
        RuleFor(m => m.Name)
            .NotNull().WithMessage("Nombre requerido")
            .NotEmpty().WithMessage("Debe especificar el nombre");

        RuleFor(m => m.FiscalNumber)
            .NotNull().WithMessage("Cédula jurídica requerido")
            .NotEmpty().WithMessage("Debe especificar la Cédula jurídica");

        RuleFor(m => m.SocialReason)
            .NotNull().WithMessage("Rason social requerido")
            .NotEmpty().WithMessage("Debe especificar la rason social");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Por favor ingrese el teléfono");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico")
            .EmailAddress().WithMessage("Por favor ingrese una dirección de correo electrónico válido");

        RuleFor(x => x.DistrictId)
            .NotEmpty().WithMessage("Por favor ingrese un distrito válido");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Por favor ingrese un distrito válido")
            .When(x => x.Address != null);
    }
}
