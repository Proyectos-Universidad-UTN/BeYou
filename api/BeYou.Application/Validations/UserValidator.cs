using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.CardId)
             .NotEmpty().WithMessage("Por favor ingrese la cédula")
             .MaximumLength(20).WithMessage("La cédula no puede tener más de 20 caracteres");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener más de 80 caracteres");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Por favor ingrese el apellido")
            .MaximumLength(80).WithMessage("El apellido no puede tener más de 80 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Por favor ingrese el correo electrónico")
            .EmailAddress().WithMessage("El correo electrónico no es válido");

        RuleFor(x => x.Telephone)
            .InclusiveBetween(10000000, 99999999).WithMessage("El teléfono debe tener 8 dígitos");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Por favor ingrese una contraseña")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("Debe seleccionar un distrito válido");

        RuleFor(x => x.Birthday)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha de nacimiento debe ser en el pasado");

        RuleFor(x => x.GenderId)
            .GreaterThan(0).WithMessage("Debe seleccionar un género válido");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Debe seleccionar un rol válido");
    }
}