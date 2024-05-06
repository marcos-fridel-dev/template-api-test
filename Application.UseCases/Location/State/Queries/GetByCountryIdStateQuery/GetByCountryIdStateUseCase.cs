using Application.Dto.Models.Location;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UseCases.Location.State.Queries.GetByCountryIdStateQuery
{
    public sealed class GetByCountryIdStateUseCase(Guid _countryId) : IRequest<List<StateDto>>
    {
        internal Guid CountryId => _countryId;
    }
}