using Application.Dto.Models.Location;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UseCases.Location.City.Queries.GetByStateIdCityQuery
{
    public sealed class GetByStateIdCityUseCase(Guid _stateId) : IRequest<List<CityDto>>
    {
        internal Guid StateId => _stateId;
    }
}