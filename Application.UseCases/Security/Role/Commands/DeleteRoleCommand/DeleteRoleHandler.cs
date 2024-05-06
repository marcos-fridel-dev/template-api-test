using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.Role;

namespace Application.UseCases.Security.Role.Commands.DeleteRoleCommand
{
    public class DeleteRoleHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<DeleteRoleUseCase, RoleDto>
    {
        public async Task<RoleDto> Handle(DeleteRoleUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity role = _unitOfWork.Roles.Delete(request.Id);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoleDto>(role);
        }
    }
}