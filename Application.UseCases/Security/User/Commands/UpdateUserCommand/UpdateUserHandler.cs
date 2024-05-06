using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.User;

namespace Application.UseCases.Security.User.Commands.UpdateUserCommand
{
    public class UpdateUserHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<UpdateUserUseCase, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity User = _unitOfWork.Users
                .Update(request.Id, _mapper.Map<TEntity>(request.User));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(User);
        }
    }
}