using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEntity = Domain.Models.Location.Currency;

namespace Application.UseCases.Location.Currency.Commands.UpdateCurrencyCommand
{
    public class UpdateCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<UpdateCurrencyUseCase, CurrencyDto>
    {
        public async Task<CurrencyDto> Handle(UpdateCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            TEntity currency = _unitOfWork.Currencies
                .Update(request.Id, _mapper.Map<TEntity>(request.Currency));
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CurrencyDto>(currency);
        }
    }
}