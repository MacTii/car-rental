using Application.Mapper.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CommentValidator : AbstractValidator<CommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(comment => comment.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(comment => comment.BlogID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("BlogID must be greater than or equal to 0");

            RuleFor(comment => comment.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Name is required and must not exceed 20 characters");

            RuleFor(comment => comment.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Surname is required and must not exceed 20 characters");

            RuleFor(comment => comment.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(comment => comment.Date)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$")
                .WithMessage("Date must be in 'yyyy-MM-dd HH:mm' format");

            RuleFor(comment => comment.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300)
                .WithMessage("Description is required and must not exceed 300 characters");
        }

    }
}
