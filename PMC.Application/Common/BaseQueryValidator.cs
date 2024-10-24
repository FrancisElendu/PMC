using FluentValidation;

namespace PMC.Application.Common
{
    public abstract class BaseQueryValidator<T> : AbstractValidator<T> where T : IPaginatedQuery, ISortableQuery
    {
        private readonly int[] _allowedPageSizes = { 5, 10, 15, 20, 30, 50 };
        private readonly string[] _allowedSortColumns = { "Role", "FirstName", "LastName" }; // Example allowed sort columns

        protected BaseQueryValidator()
        {
            RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => _allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", _allowedPageSizes)}]");
            
            RuleFor(r => r.SortColumn)
                .Must(value => _allowedSortColumns.Contains(value))
                .WithMessage($"Sort column must be one of [{string.Join(",", _allowedSortColumns)}]");

            RuleFor(r => r.SortDirection)
                .Must(value => value == "asc" || value == "desc")
                .WithMessage("Sort direction must be 'asc' or 'desc'");
        }
    }
}
