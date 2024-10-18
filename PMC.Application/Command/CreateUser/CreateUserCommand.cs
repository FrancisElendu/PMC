using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.CreateUser
{
    public  class CreateUserCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } // Unique
        public string? PasswordHash { get; set; }
        public string? Role { get; set; } // Admin, Doctor, Pharmacist, Patient
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
