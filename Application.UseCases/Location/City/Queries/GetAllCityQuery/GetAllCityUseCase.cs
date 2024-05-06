using Application.Dto.Models.Location;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Location.City.Queries.GetAllCityQuery
{
    public sealed class GetAllCityUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : IRequest<IEnumerable<CityDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}