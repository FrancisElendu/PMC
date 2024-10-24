using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using PMC.Domain.Exceptions;
using PMC.Application.Common;

namespace PMC.Application.Queries.GetUsersByCondition
{
    public class GetUsersByConditionQueryHandler(ILogger<GetUsersByConditionQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetUsersByConditionQuery, PagedResult<UserDto>>
    {
        public async Task<PagedResult<UserDto?>> Handle(GetUsersByConditionQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting registered users by filtering on the firstname euqals {predicate}", request.Filter);

            // Check for invalid inputs
            if (string.IsNullOrWhiteSpace(request.Column) || string.IsNullOrWhiteSpace(request.Filter))
            {
                logger.LogWarning("Invalid column or filter value provided.");
                throw new BadRequestException("Column and filter values must not be null or empty.");
            }

            // Ensure the column exists on the User entity
            var propertyInfo = typeof(User).GetProperty(request.Column, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                logger.LogWarning("The column '{column}' does not exist on the User entity.", request.Column);
                throw new NotFoundException($"The column '{request.Column}' does not exist on the User entity.");
            }

            // Build a dynamic predicate based on column name
            var parameter = Expression.Parameter(typeof(User), "e");
            var property = Expression.Property(parameter, request.Column);
            var constant = Expression.Constant(request.Filter);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExpression = Expression.Call(property, containsMethod!, constant);

            var predicate = Expression.Lambda<Func<User, bool>>(containsExpression, parameter);

            // Query User repository and get users matching the condition
            var (users, totalCount) = await _repo.FindAsync(predicate, request.PageNumber, request.PageSize,
                                                request.SortColumn,
                                                request.SortDirection);

            if (users is null || !users.Any())
                //return Enumerable.Empty<UserDto>();
            throw new NotFoundException($"The column '{request.Column} or filter '{request.Filter}' does not exist on the User entity.");

            // Map User entities to UserDto
            var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
            var result = new PagedResult<UserDto>(usersDtos, totalCount, request.PageSize, request.PageNumber);
            return result;


        }
    }
}
