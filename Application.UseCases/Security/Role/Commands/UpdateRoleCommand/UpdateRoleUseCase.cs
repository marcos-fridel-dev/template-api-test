using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.Role.Commands.UpdateRoleCommand
{
    public sealed class UpdateRoleUseCase(Guid _id, RoleDto _role) : IRequest<RoleDto>
    {
        internal Guid Id => _id;
        internal RoleDto Role => _role;
    }
}