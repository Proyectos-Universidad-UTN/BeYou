using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Por favor ingrese el nombre")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Por favor ingrese el apellido")
                .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Por favor ingrese el correo electrónico")
                .EmailAddress().WithMessage("Por favor ingrese una dirección de correo electrónico válida");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Por favor ingrese el teléfono")
                .GreaterThan(0).WithMessage("El número de teléfono debe ser mayor a cero");

            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage("Por favor seleccione un distrito válido");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("La dirección no puede tener más de 200 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));
        }
    }
}
