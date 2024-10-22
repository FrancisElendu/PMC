using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Dtos;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;

namespace PMC.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting registered users by {userId}", request.Id);
            var user = await _repo.GetByIdAsync(request.Id);
            var userDtos = mapper.Map<UserDto>(user);
            return userDtos;
        }
    }
}
