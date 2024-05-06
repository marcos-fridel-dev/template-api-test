using Application.Dto.Models.Security;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Security.User.Queries.GetAllUserQuery
{
    public class GetAllUserHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllUserUseCase, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUserUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<UserDto>>(
                await _unitOfWork.Users
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}