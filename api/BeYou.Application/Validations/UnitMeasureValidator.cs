using BeYou.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Application.Validations;

public class UnitMeasureValidator : AbstractValidator<UnitMeasure>
{
    public UnitMeasureValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(25).WithMessage("El nombre no puede tener mas de 25 caracteres");

        RuleFor(x => x.Symbol)
            .NotEmpty().WithMessage("Por favor ingrese el símbolo")
            .MaximumLength(5).WithMessage("El símbolo no puede tener mas de 5 caracteres");
    }
}