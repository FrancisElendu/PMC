using MediatR;
using PMC.Application.Dtos;

namespace PMC.Application.Queries.GetUserById
{
    public class GetUserByIdQuery(int id) : IRequest<UserDto?>
    {
        public int Id { get; } = id;
    }
}
