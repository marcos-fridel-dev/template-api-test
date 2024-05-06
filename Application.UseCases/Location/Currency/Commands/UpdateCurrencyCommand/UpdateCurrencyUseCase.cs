using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.Currency.Commands.UpdateCurrencyCommand
{
    public sealed class UpdateCurrencyUseCase(Guid _id, CurrencyPostDto _currency) : IRequest<CurrencyDto>
    {
        internal Guid Id => _id;
        internal CurrencyPostDto Currency => _currency;

    }
}