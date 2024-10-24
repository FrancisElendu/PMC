using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Common;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;

namespace PMC.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetAllUsersQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all registered users");
            var (users, totalCount) = await _repo.GetAllAsync(request.PageNumber, request.PageSize);
            if(users != null && users.Any())
            {
                var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);

                var result = new PagedResult<UserDto>(usersDtos, totalCount, request.PageSize, request.PageNumber);
                return result;   
            }
            // Return an empty PagedResult with an empty list of UserDto
            return new PagedResult<UserDto>(Enumerable.Empty<UserDto>(), 0, request.PageSize, request.PageNumber);
        }
    }
}
