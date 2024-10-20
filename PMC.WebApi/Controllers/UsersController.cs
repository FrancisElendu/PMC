using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMC.Application.Command.CreateUser;
using PMC.Application.Command.DeleteUser;
using PMC.Application.Command.UpdateUser;
using PMC.Application.Dtos;
using PMC.Application.Queries.GetAllUsers;
using PMC.Application.Queries.GetUserById;
using PMC.Application.Queries.GetUsersByCondition;
using System.Linq.Expressions;
using System.Xml;

namespace PMC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            
            if(user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByPredicate(string filter)
        {
            // Sample predicate: modify as per your requirements
            //Expression<Func<UserDto, bool>> predicate = e => e.FirstName.Contains(filter);
            //var user = await mediator.Send(new GetUsersByConditionQuery1(predicate));

            var user = await mediator.Send(new GetUsersByConditionQuery(filter));

            if (user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserCommand command)
        {
            command.UserId = id;
            var isUserUpdated = await mediator.Send(command);

            if (isUserUpdated)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteUserCommand(id));

            if (isDeleted)
                return NoContent();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
