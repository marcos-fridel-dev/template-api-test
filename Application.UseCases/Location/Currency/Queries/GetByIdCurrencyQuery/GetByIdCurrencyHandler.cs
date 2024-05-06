using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Currency.Queries.GetByIdCurrencyQuery
{
    public class GetByIdCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdCurrencyUseCase, CurrencyDto?>
    {
        public async Task<CurrencyDto?> Handle(GetByIdCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<CurrencyDto>(await _unitOfWork.Currencies.GetByIdIncludeAsync(request.Id));
        }
    }
}