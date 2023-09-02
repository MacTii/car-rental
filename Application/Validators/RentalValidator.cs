using Application.Mapper.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class RentalValidator : AbstractValidator<RentalDTO>
    {
        public RentalValidator()
        {
            RuleFor(rental => rental.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(rental => rental.CarID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CarID must be greater than or equal to 0");

            RuleFor(rental => rental.UserID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("UserID must be greater than or equal to 0");

            RuleFor(rental => rental.RentDate)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$")
                .WithMessage("Rent date must be in 'yyyy-MM-dd HH:mm' format");

            RuleFor(rental => rental.ReturnDate)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$")
                .WithMessage("Return date must be in 'yyyy-MM-dd HH:mm' format");

            RuleFor(rental => rental.Comment)
                .MaximumLength(80)
                .WithMessage("Comment must not exceed 80 characters");

            RuleFor(rental => rental.PaymentMethod)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Payment method is required and must not exceed 20 characters");
        }
    }
}
