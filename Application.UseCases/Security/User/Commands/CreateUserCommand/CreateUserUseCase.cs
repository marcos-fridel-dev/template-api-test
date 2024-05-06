using Application.Dto.Models.Security;
using MediatR;

namespace Application.UseCases.Security.User.Commands.CreateUserCommand
{
    public sealed class CreateUserUseCase(UserDto _role) : IRequest<UserDto>
    {
        internal UserDto User => _role;
    }
}