using MediatR;
using PMC.Application.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;
using PMC.Application.Dtos;

namespace PMC.Application.Command.CreateUser
{
    public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<CreateUserCommand, int>  //IApiService apiService, 
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new user");

            var user = mapper.Map<User>(request);
            await _repo.AddAsync(user);
            //var userToCreateUserCommand = mapper.Map<CreateUserCommand>(user);
            return user.UserId;


            //var userCreated = await apiService.PostAsync<User, User>("posts", user);

            //return userCreated.UserId;
        }
    }
}
