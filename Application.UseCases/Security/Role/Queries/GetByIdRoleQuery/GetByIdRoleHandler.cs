using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Security.Role.Queries.GetByIdRoleQuery
{
    public class GetByIdRoleHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdRoleUseCase, RoleDto?>
    {
        public async Task<RoleDto?> Handle(GetByIdRoleUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<RoleDto>(await _unitOfWork.Roles.GetByIdIncludeAsync(request.Id));
        }
    }
}