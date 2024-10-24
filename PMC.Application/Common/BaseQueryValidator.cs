using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Common
{
    public abstract class BaseQueryValidator<T> : AbstractValidator<T> where T :  IPaginatedQuery
    {
        private readonly int[] _allowedPageSizes = { 5, 10, 15, 20, 30, 50 };

        protected BaseQueryValidator()
        {
            RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => _allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", _allowedPageSizes)}]");
        }
    }
}
