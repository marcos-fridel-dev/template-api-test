using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Security.User;

namespace Application.UseCases.Security.User.Commands.DeleteUserCommand
{
    public class DeleteUserHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<DeleteUserUseCase, UserDto>
    {
        public async Task<UserDto> Handle(DeleteUserUseCase request, CancellationToken cancellationToken = default)
        {
            UserDto currencyDto = _mapper.Map<UserDto>(await _unitOfWork.Users.GetByIdAndNotDeletedAsync(request.Id));

            TEntity role = _unitOfWork.Users
                .Delete(request.Id);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(role);
        }
    }
}