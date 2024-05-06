using Application.Dto.Models.Security;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Security.User.Queries.GetAllUserQuery
{
    public sealed class GetAllUserUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : IRequest<IEnumerable<UserDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}