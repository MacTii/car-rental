using Application.Mapper.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(user => user.UserCredentialsID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("UserCredentialsID must be greater than or equal to 0");

            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Name is required and must not exceed 20 characters");

            RuleFor(user => user.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Surname is required and must not exceed 20 characters");

            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Email is required and must not exceed 30 characters");

            RuleFor(user => user.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Phone number is required and must not exceed 20 characters");

            RuleFor(user => user.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Address is required");

            RuleFor(user => user.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .WithMessage("Date of birth is required");

            RuleFor(user => user.Gender)
                .NotNull()
                .NotEmpty()
                .WithMessage("Gender is required");

            RuleFor(user => user.IdentificationNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .WithMessage("Identification number is required and must not exceed 15 characters");

            RuleFor(user => user.DrivingLicenseNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Driving license number is required and must not exceed 20 characters");
        }

    }
}
