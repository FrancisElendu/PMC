using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQueryHandler(ILogger<GetUsersByConditionQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetUsersByConditionQuery, IEnumerable<UserDto?>>
    {
        public async Task<IEnumerable<UserDto?>> Handle(GetUsersByConditionQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting registered users by predicate");

            //IQueryable<IEnumerable<User>> usersQuery = _repo.Query();
            IQueryable<User> usersQuery = _repo.Query();

            if (request.Predicate != null)
            {
                var userDtos = await usersQuery
                .ProjectTo<UserDto>(mapper.ConfigurationProvider) // AutoMapper will handle the mapping
                .Where(request.Predicate) // Apply the predicate on UserDto
                .ToListAsync(cancellationToken); // Execute the query

                return userDtos;
            }

            //var userDtos = mapper.Map<IEnumerable<UserDto>>(user);
            return Enumerable.Empty<UserDto>();
        }
    }
}
