using MediatR;
using PMC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetUserById
{
    public class GetUserByIdQuery(int id) :IRequest<UserDto?>
    {
        public int Id { get; } = id;
    }
}
