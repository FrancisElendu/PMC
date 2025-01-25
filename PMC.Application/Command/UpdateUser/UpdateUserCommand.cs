using MediatR;
using PMC.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest, IUserCommand
    {
        public int UserId { get; set; } // PK
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public string? Email { get; set; } // Unique
        //public string? Role { get; set; } // Admin, Doctor, Pharmacist, Patient
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } // Unique
        public string? PasswordHash { get; set; }
        public string? Role { get; set; } // Admin, Doctor, Pharmacist, Patient
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
