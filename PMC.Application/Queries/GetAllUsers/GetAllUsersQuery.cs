using MediatR;
using PMC.Application.Common;
using PMC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PagedResult<UserDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
