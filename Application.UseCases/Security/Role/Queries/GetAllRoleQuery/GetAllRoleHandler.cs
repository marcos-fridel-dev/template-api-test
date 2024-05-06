using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Security.Role.Queries.GetAllRoleQuery
{
    public class GetAllRoleHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllRoleUseCase, IEnumerable<RoleDto>>
    {
        public async Task<IEnumerable<RoleDto>> Handle(GetAllRoleUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<RoleDto>>(
                await _unitOfWork.Roles
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}