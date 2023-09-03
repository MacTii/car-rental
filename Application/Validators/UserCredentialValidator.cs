using Application.Mapper.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UserCredentialValidator : AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialValidator()
        {
            RuleFor(userCredential => userCredential.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(userCredential => userCredential.Username)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10)
                .WithMessage("Username is required and must not exceed 10 characters");

            RuleFor(userCredential => userCredential.UserRole)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("UserRole is required and must not exceed 50 characters");
        }
    }
}
