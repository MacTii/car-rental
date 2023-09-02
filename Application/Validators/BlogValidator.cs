using Application.Mapper.DTOs;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class BlogValidator : AbstractValidator<BlogDTO>
    {
        public BlogValidator()
        {
            RuleFor(blog => blog.ID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ID must be greater than or equal to 0");

            RuleFor(blog => blog.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Title is required and must not exceed 50 characters");

            RuleFor(blog => blog.AuthorName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Author's name is required and must not exceed 20 characters");

            RuleFor(blog => blog.AuthorSurname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Author's surname is required and must not exceed 20 characters");

            RuleFor(blog => blog.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("Description is required and must not exceed 250 characters");

            RuleFor(blog => blog.Date)
                .NotNull()
                .NotEmpty()
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$")
                .WithMessage("Date must be in 'yyyy-MM-dd HH:mm' format");

            RuleFor(blog => blog.Image)
                .NotNull()
                .NotEmpty()
                .WithMessage("Image is required");

            RuleFor(blog => blog.DetailedDescription)
                .NotNull()
                .NotEmpty()
                .MaximumLength(700)
                .WithMessage("Detailed description is required and must not exceed 700 characters");
        }
    }
}
