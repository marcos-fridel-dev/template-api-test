using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Country.Queries.GetAllCountryQuery
{
    public class GetAllCountryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllCountryUseCase, IEnumerable<CountryDto>>
    {
        public async Task<IEnumerable<CountryDto>> Handle(GetAllCountryUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<CountryDto>>(
                await _unitOfWork.Countries
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}