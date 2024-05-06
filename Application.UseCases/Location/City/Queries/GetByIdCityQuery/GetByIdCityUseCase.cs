using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.City.Queries.GetByIdCityQuery
{
    public sealed class GetByIdCityUseCase(Guid _id) : IRequest<CityDto?>
    {
        internal Guid Id => _id;
    }
}