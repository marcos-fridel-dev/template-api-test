using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.Currency.Queries.GetByIdCurrencyQuery
{
    public sealed class GetByIdCurrencyUseCase(Guid _id) : IRequest<CurrencyDto?>
    {
        internal Guid Id => _id;
    }
}