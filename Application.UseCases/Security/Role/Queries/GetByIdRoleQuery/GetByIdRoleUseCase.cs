using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.Role.Queries.GetByIdRoleQuery
{
    public sealed class GetByIdRoleUseCase(Guid _id) : IRequest<RoleDto?>
    {
        internal Guid Id => _id;
    }
}