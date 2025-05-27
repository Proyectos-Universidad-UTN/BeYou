using BeYou.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Application.Validations;

public class ReservationDetailValidator : AbstractValidator<ReservationDetail>
{
    public ReservationDetailValidator()
    {
        RuleFor(m => m.ReservationId)
          .NotEmpty().WithMessage("Debe especificar la reserva");

        RuleFor(m => m.ReservationId)
            .NotEmpty().WithMessage("Debe especificar el servicio");
    }
}
