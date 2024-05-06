using Application.Dto.Models.Security;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Security.Role.Queries.GetAllRoleQuery
{
    public sealed class GetAllRoleUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : IRequest<IEnumerable<RoleDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}