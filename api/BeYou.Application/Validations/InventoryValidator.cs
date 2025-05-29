using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;
public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
    }
}
