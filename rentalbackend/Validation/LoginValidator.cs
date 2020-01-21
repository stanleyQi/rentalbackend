using FluentValidation;
using rentalbackend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rentalbackend.Validation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
      .WithMessage("Email is mandatory.");

            RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage("Password is mandatory.");
        }
    }
}
