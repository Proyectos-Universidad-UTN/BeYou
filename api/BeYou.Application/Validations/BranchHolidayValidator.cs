using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class BranchHolidayValidator : AbstractValidator<BranchHoliday>
{
    public BranchHolidayValidator()
    {
        RuleFor(m => m.HolidayId)
            .NotEmpty().WithMessage("Debe especificar el feriado");

        RuleFor(m => m.BranchId)
            .NotEmpty().WithMessage("Debe especificar la sucursal");

        RuleFor(m => m.Year)
            .GreaterThanOrEqualTo((short)DateTime.Now.Year).WithMessage(m => $"Año({m.Year}) no puede ser menor al actual");
    }
}
