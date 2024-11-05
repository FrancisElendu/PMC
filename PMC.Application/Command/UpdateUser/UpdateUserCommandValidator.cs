using FluentValidation;
using PMC.Application.Common;

namespace PMC.Application.Command.UpdateUser
{
    //public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    public class UpdateUserCommandValidator : BaseCommandValidator<UpdateUserCommand>
    {
        
        //private readonly List<string?> validRoles = ["Admin", "Doctor", "Pharmacist", "Patient"];
        public UpdateUserCommandValidator() : base()
        {
            //var roles = string.Join(", ", validRoles);
            //RuleFor(x => x.Role)
            // .Must(validRoles.Contains)
            // .WithMessage($"Invalid role. Please enter a valid role: {roles}");

            //RuleFor(x => x.Email)
            //    .EmailAddress()
            //    .WithMessage("Please provide a valid email address.");

            //RuleFor(x => x.FirstName)
            //    .Length(3, 50);

            //RuleFor(x => x.LastName)
            //    .Length(3, 50);
        }
    }
}
