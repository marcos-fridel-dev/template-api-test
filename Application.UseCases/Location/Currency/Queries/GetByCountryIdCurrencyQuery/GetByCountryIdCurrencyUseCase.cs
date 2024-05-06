using Application.Dto.Models.Location;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UseCases.Location.Currency.Queries.GetByCountryIdCurrencyQuery
{
    public sealed class GetByCountryIdCurrencyUseCase(Guid _countryId) : IRequest<List<CurrencyDto>>
    {
        internal Guid CountryId => _countryId;

    }
}