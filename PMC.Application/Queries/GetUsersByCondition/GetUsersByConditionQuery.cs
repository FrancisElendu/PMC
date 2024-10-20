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
    //public class GetUsersByConditionQuery1(Expression<Func<UserDto?, bool>> predicate) : IRequest<IEnumerable<UserDto>>
    //{
    //    public Expression<Func<UserDto?, bool>> Predicate { get; } = predicate;
    //}

    public class GetUsersByConditionQuery(string filter) : IRequest<IEnumerable<UserDto>>
    {
        public string Filter { get; } = filter;
    }
}
