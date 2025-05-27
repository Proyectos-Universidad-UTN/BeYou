using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class InventoryProductValidator : AbstractValidator<InventoryProduct>
{
    public InventoryProductValidator()
    {
        RuleFor(m => m.Mininum)
            .GreaterThan(0).WithMessage("Cantidad mínima debe ser mayor a 0")
            .LessThanOrEqualTo(x => x.Maximum).WithMessage("Cantidad mínima debe ser menor a la cantidad máxima");

        RuleFor(m => m.Maximum)
            .GreaterThan(0).WithMessage("Cantidad máxima debe ser mayor a 0");
    }
}
