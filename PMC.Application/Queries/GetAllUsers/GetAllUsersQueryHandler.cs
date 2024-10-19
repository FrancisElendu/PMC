using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;

namespace PMC.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userDtos = new UserDto { };
            logger.LogInformation("Getting all registered users");
            var users = await _repo.GetAllAsync();
            if(users != null && users.Any())
            {
                var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
                return usersDtos;   
            }
            return Enumerable.Empty<UserDto>();
        }
    }
}
