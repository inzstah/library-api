﻿using FluentValidation;
using Library.Domain.Data;
namespace Library.Application.Validation;

public class RegisterValidator : AbstractValidator<User>
{
    public RegisterValidator() {
        
        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            
            }
}
