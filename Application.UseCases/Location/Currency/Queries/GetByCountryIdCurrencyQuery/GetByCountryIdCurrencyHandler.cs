using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Currency.Queries.GetByCountryIdCurrencyQuery
{
    public class GetByCountryIdCurrencyHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByCountryIdCurrencyUseCase, List<CurrencyDto>>
    {
        public async Task<List<CurrencyDto>> Handle(GetByCountryIdCurrencyUseCase request, CancellationToken cancellationToken = default)
        {
            return
                _mapper.Map<List<CurrencyDto>>(
                    await _unitOfWork.Currencies.Entity
                        .Where(x => x.CountryId == request.CountryId && !x.IsDeleted)
                        .OrderBy(x => x.Name)
                        .ToListAsync());
        }
    }
}