using FluentValidation;
using PMC.Application.Common;
using PMC.Application.Queries.GetAllUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQueryValidator : BaseQueryValidator<GetUsersByConditionQuery>
    {
        public GetUsersByConditionQueryValidator() :base()
        {
        }
    }
}
