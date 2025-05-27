using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class ScheduleValidator : AbstractValidator<Schedule>
{
    public ScheduleValidator()
    {
        RuleFor(x => x.Day)
            .IsInEnum();

        RuleFor(x => x.StartHour)
            .NotEmpty().WithMessage("La hora de inicio es requerida")
            .NotNull().WithMessage("La hora de inicio no puede ser nula");

        RuleFor(x => x.EndHour)
            .NotEmpty().WithMessage("La hora de fin es requerida")
            .NotNull().WithMessage("La hora de fin no puede ser nula")
            .GreaterThan(x => x.StartHour)
            .WithMessage("La hora de fin debe ser mayor que la hora de inicio");
    }
}