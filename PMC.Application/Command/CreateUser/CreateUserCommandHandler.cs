using MediatR;
using PMC.Application.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;
using PMC.Domain.Entities;

namespace PMC.Application.Command.CreateUser
{
    public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IMapper mapper, IApiService apiService) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new user");

            var user = mapper.Map<User>(request);

            var userCreated = await apiService.PostAsync<User, User>("posts", user);

            return userCreated.UserId;
        }
    }
}
