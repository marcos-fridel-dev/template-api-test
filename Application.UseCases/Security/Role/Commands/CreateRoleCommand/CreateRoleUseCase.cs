using Application.Dto.Models.Security;
using MediatR;

namespace Application.UseCases.Security.Role.Commands.CreateRoleCommand
{
    public sealed class CreateRoleUseCase(RoleDto _role) : IRequest<RoleDto>
    {
        internal RoleDto Role => _role;

    }
}