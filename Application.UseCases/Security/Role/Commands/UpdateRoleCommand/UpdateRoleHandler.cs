using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.Role;

namespace Application.UseCases.Security.Role.Commands.UpdateRoleCommand
{
    public class UpdateRoleHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<UpdateRoleUseCase, RoleDto>
    {
        public async Task<RoleDto> Handle(UpdateRoleUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity role = _unitOfWork.Roles
                .Update(request.Id, _mapper.Map<TEntity>(request.Role));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoleDto>(role);
        }
    }
}