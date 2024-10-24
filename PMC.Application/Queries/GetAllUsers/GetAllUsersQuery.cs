using MediatR;
using PMC.Application.Common;
using PMC.Application.Dtos;

namespace PMC.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PagedResult<UserDto>>, IPaginatedQuery, ISortableQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; } = "Id"; // Default sorting column
        public string SortDirection { get; set; } = "asc"; // Default sorting direction
    }
}
