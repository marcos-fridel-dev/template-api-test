using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Currency.Commands.DeleteCurrencyCommand
{
    public class DeleteCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<DeleteCurrencyUseCase, CurrencyDto>
    {
        public async Task<CurrencyDto> Handle(DeleteCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            CurrencyDto currencyDto = _mapper.Map<CurrencyDto>(await _unitOfWork.Currencies.GetByIdAndNotDeletedAsync(request.Id));

            Domain.Models.Location.Currency currency = _unitOfWork.Currencies
                .Delete(request.Id);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CurrencyDto>(currency);
        }
    }
}