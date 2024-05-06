using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.Country.Queries.GetByIdCountryQuery
{
    public sealed class GetByIdCountryUseCase(Guid _id) : IRequest<CountryDto?>
    {
        internal Guid Id => _id;
    }
}