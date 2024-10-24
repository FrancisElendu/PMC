using MediatR;
using PMC.Application.Common;
using PMC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQuery() : IRequest<PagedResult<UserDto>> , IPaginatedQuery  
    {
        public string? Filter { get; set; }
        public string? Column { get; set; } 
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }
    
}
