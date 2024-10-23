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

            return Ok(user);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByPredicate(string column, string filter)
        {
            var user = await mediator.Send(new GetUsersByConditionQuery(column, filter));

            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserCommand command)
        {
            command.UserId = id;
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
