using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.Role.Commands.DeleteRoleCommand
{
    public sealed class DeleteRoleUseCase(Guid _id) : IRequest<RoleDto>
    {
        internal Guid Id => _id;
    }
}