using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Por favor ingrese la fecha")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha debe ser hoy o una fecha futura");

        RuleFor(x => x.Hour)
            .NotEmpty().WithMessage("Por favor ingrese la hora");

        RuleFor(x => x.BranchId)
            .GreaterThan((byte)0).WithMessage("Por favor ingrese un Id de sucursal válido");
    }
}