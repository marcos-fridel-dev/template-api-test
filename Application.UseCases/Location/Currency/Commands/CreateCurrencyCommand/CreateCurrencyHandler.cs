using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Location.Currency;

namespace Application.UseCases.Location.Currency.Commands.CreateCurrencyCommand
{
    internal class CreateCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreateCurrencyUseCase, CurrencyDto>
    {
        public async Task<CurrencyDto> Handle(CreateCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity currency = _unitOfWork.Currencies
                .Add(_mapper.Map<TEntity>(request.Currency));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CurrencyDto>(currency);
        }
    }
}