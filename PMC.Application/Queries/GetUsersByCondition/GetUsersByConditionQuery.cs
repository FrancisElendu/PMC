using MediatR;
using PMC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQuery(string column, string filter) : IRequest<IEnumerable<UserDto>>
    {
        public string Filter { get; } = filter;
        public string Column { get; } = column;
    }
}
