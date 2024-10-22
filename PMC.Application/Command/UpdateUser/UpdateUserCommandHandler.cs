using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMC.Application.Command.CreateUser;
using PMC.Application.Command.DeleteUser;
using PMC.Domain.Entities;
using PMC.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Command.UpdateUser
{
    public class UpdateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IMapper mapper, IRepository<User> _repo) : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating user with id: {userId} with {@UpdatedUser}", request.UserId, request);
            var user = await _repo.GetByIdAsync(request.UserId);
            if (user is null)
                return false;

            var res = mapper.Map(request, user);

            await _repo.UpdateAsync(res);

            return true;
        }
    }
}
