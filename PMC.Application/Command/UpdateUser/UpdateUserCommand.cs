using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; } // PK
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } // Unique
        public string? Role { get; set; } // Admin, Doctor, Pharmacist, Patient
    }
}
