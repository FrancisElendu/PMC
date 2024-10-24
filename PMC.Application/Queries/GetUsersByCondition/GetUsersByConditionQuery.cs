using MediatR;
using PMC.Application.Common;
using PMC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQuery(string column, string filter, int pageNumber, int pageSize) : IRequest<PagedResult<UserDto>>
    {
        public string Filter { get; } = filter;
        public string Column { get; } = column;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
    }
    
}
