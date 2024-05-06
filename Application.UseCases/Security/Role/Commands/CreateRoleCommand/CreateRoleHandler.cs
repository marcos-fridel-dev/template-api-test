using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.Role;

namespace Application.UseCases.Security.Role.Commands.CreateRoleCommand
{
    public class CreateRoleHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreateRoleUseCase, RoleDto>
    {
        public async Task<RoleDto> Handle(CreateRoleUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity Role = _unitOfWork.Roles
                .Add(_mapper.Map<TEntity>(request.Role));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoleDto>(Role);
        }
    }
}