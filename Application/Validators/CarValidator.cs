using Application.Mapper.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CarValidator : AbstractValidator<CarDTO>
    {
        public CarValidator()
        {
            RuleFor(car => car.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(car => car.Make)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Make is required and must not exceed 50 characters");

            RuleFor(car => car.Model)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Model is required and must not exceed 50 characters");

            RuleFor(car => car.RegistrationNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Registration number is required and must not exceed 15 characters");

            RuleFor(car => car.Color)
                .NotNull()
                .NotEmpty()
                .WithMessage("Color is required");

            RuleFor(car => car.PricePerDay)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price per day is required");

            RuleFor(car => car.Year)
                .GreaterThanOrEqualTo(1900)
                .WithMessage("Year must be greater than or equal to 1900");

            RuleFor(car => car.Engine)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Engine is required and must not exceed 15 characters");

            RuleFor(car => car.Speed)
                .GreaterThan(0)
                .WithMessage("Speed must be greater than 0");

            RuleFor(car => car.Image)
                .NotNull()
                .NotEmpty()
                .WithMessage("Image is required");

            RuleFor(car => car.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(400)
                .WithMessage("Description is required and must not exceed 400 characters");

            RuleFor(car => car.GPS)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("GPS is required and must not exceed 20 characters");

            RuleFor(car => car.Ratings)
                .NotNull()
                .WithMessage("Ratings are required");

            RuleFor(car => car.SeatType)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Seat type is required and must not exceed 15 characters");
        }

    }
}
