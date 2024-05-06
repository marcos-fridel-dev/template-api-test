using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Security.User.Queries.GetByIdUserQuery
{
    public class GetByIdUserHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdUserUseCase, UserDto?>
    {
        public async Task<UserDto?> Handle(GetByIdUserUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<UserDto>(await _unitOfWork.Users.GetByIdIncludeAsync(request.Id));
        }
    }
}