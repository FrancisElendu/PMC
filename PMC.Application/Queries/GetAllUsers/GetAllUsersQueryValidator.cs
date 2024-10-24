using FluentValidation;
using PMC.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryValidator : BaseQueryValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator() : base()
        {
        }
    }
}
