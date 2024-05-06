using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.Currency.Commands.DeleteCurrencyCommand
{
    public sealed class DeleteCurrencyUseCase(Guid _id) : IRequest<CurrencyDto>
    {
        internal Guid Id => _id;

    }
}