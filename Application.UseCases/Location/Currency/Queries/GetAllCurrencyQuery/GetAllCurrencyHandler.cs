using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Currency.Queries.GetAllCurrencyQuery
{
    public class GetAllCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllCurrencyUseCase, IEnumerable<CurrencyDto>>
    {
        public async Task<IEnumerable<CurrencyDto>> Handle(GetAllCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<CurrencyDto>>(
                await _unitOfWork.Currencies
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}