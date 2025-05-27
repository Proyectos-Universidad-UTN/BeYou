using BeYou.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Application.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener más de 80 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("La descripción no puede tener más de 150 caracteres");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Por favor ingrese la marca")
            .MaximumLength(50).WithMessage("La marca no puede tener más de 50 caracteres");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Por favor ingrese una categoría válida");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El costo debe ser mayor a 0");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Por favor ingrese el SKU")
            .MaximumLength(30).WithMessage("El SKU no puede tener más de 30 caracteres");

        RuleFor(x => x.UnitMeasureId)
            .NotEmpty().WithMessage("Por favor ingrese una unidad de medida válida");
    }
}