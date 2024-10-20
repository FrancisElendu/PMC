using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.DeleteUser
{
    public class DeleteUserCommand(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }
}
