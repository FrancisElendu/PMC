using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Command.CreateUser;
using PMC.Domain.Entities;
using PMC.Domain.Exceptions;
using PMC.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.DeleteUser
{
    public class DeleteUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IRepository<User> _repo) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting user with id : {request.Id}", request.Id);
            var user = await _repo.GetByIdAsync(request.Id );
            if ( user is null )
                throw new NotFoundException($"User with {request.Id} does not exist");

            await _repo.RemoveAsync(user);
        }
    }
}
