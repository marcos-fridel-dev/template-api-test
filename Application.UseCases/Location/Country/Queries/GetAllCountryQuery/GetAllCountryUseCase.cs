using Application.Dto.Models.Location;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Location.Country.Queries.GetAllCountryQuery
{
    public sealed class GetAllCountryUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : GetAllCountryCaching, IRequest<IEnumerable<CountryDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}