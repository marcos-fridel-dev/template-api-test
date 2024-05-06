using Application.Dto.Models.Location;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Location.Currency.Queries.GetAllCurrencyQuery
{
    public sealed class GetAllCurrencyUseCase(int _pageNumber, int _pageSize, IsDeleted _isDeleted) : IRequest<IEnumerable<CurrencyDto>>
    {
        internal int PageNumber => _pageNumber;
        internal int PageSize => _pageSize;
        internal IsDeleted IsDeleted => _isDeleted;
    }
}