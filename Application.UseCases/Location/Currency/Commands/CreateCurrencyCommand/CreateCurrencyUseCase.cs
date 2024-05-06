using Application.Dto.Models.Location;
using MediatR;

namespace Application.UseCases.Location.Currency.Commands.CreateCurrencyCommand
{
    public sealed class CreateCurrencyUseCase(CurrencyPostDto _currency) : IRequest<CurrencyDto>
    {
        internal CurrencyPostDto Currency => _currency;

    }
}