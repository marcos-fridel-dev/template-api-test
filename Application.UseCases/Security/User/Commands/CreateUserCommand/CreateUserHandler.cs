using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.User;

namespace Application.UseCases.Security.User.Commands.CreateUserCommand
{
    public class CreateUserHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreateUserUseCase, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity user = _unitOfWork.Users
                .Add(_mapper.Map<TEntity>(request.User));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}