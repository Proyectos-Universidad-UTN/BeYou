using BeYou.Domain.Models;
using FluentValidation;


namespace BeYou.Application.Validations;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
    }
}
