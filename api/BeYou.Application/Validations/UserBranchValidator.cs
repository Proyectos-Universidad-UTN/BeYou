using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class UserBranchValidator : AbstractValidator<UserBranch>
{
    public UserBranchValidator()
    {
        RuleFor(m => m.BranchId)
            .NotEqual((byte)0).WithMessage("Debe indicar una sucursal");

        RuleFor(m => m.UserId)
            .NotEqual((short)0).WithMessage("Debe indicar el usuario");
    }
}
