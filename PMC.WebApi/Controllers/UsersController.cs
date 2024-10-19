using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMC.Application.Command.CreateUser;
using PMC.Application.Queries.GetAllUsers;
using PMC.Application.Queries.GetUserById;

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

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
