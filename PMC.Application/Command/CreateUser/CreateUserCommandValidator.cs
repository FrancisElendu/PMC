using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PMC.Domain.Entities;

namespace PMC.Application.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly List<string> validRoles = ["Admin", "Doctor", "Pharmacist", "Patient"];
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Role)
             .Must(validRoles.Contains)
             .WithMessage("Invalid role. Please enter a valid role");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address.");

            RuleFor(x => x.FirstName)
                .Length(3, 50);

            RuleFor(x => x.LastName)
                .Length(3, 50);
        }
    }
}
