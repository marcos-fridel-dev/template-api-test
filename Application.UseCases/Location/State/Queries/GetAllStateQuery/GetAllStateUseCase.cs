using Application.Dto.Models.Location;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Location.State.Queries.GetAllStateQuery
{
    public sealed class GetAllStateUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : IRequest<IEnumerable<StateDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}