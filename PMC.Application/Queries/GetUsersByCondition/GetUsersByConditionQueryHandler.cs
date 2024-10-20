using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PMC.Application.Queries.GetUsersByCondition
{
    //public class GetUsersByConditionQueryHandler1(ILogger<GetUsersByConditionQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetUsersByConditionQuery1, IEnumerable<UserDto?>>
    //{
    //    public async Task<IEnumerable<UserDto?>> Handle(GetUsersByConditionQuery request, CancellationToken cancellationToken)
    //    {
    //        logger.LogInformation("Getting registered users by predicate");

    //        //IQueryable<IEnumerable<User>> usersQuery = _repo.Query();
    //        IQueryable<User> usersQuery = _repo.Query();

    //        if (request.Predicate != null)
    //        {
    //            var userDtos = await usersQuery
    //            .ProjectTo<UserDto>(mapper.ConfigurationProvider) // AutoMapper will handle the mapping
    //            .Where(request.Predicate) // Apply the predicate on UserDto
    //            .ToListAsync(cancellationToken); // Execute the query

    //            return userDtos;
    //        }

    //        //var userDtos = mapper.Map<IEnumerable<UserDto>>(user);
    //        return Enumerable.Empty<UserDto>();
    //    }
    //}

    public class GetUsersByConditionQueryHandler(ILogger<GetUsersByConditionQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetUsersByConditionQuery, IEnumerable<UserDto?>>
    {
        public async Task<IEnumerable<UserDto?>> Handle(GetUsersByConditionQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting registered users by predicate");


            // Build predicate for User entity
            Expression<Func<User, bool>> predicate = e => e.FirstName.Contains(request.Filter);

            // Query User repository and get users matching the condition
            var users = await _repo.FindAsync(predicate);

            if (users is null || !users.Any())
                return Enumerable.Empty<UserDto>();

            // Map User entities to UserDto
            return mapper.Map<IEnumerable<UserDto>>(users);

            ////IQueryable<IEnumerable<User>> usersQuery = _repo.Query();
            //IQueryable<User> usersQuery = _repo.Query();

            //if (request.Predicate != null)
            //{
            //    var userDtos = await usersQuery
            //    .ProjectTo<UserDto>(mapper.ConfigurationProvider) // AutoMapper will handle the mapping
            //    .Where(request.Predicate) // Apply the predicate on UserDto
            //    .ToListAsync(cancellationToken); // Execute the query

            //    return userDtos;
            //}

            ////var userDtos = mapper.Map<IEnumerable<UserDto>>(user);
            //return Enumerable.Empty<UserDto>();
        }
    }
}
