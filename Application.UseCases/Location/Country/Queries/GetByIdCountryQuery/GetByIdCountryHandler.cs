using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.Country.Queries.GetByIdCountryQuery
{
    public class GetByIdCountryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdCountryUseCase, CountryDto?>
    {
        public async Task<CountryDto?> Handle(GetByIdCountryUseCase request, CancellationToken cancellationToken = default)
        {
            CountryDto countryDto = _mapper.Map<CountryDto>(await _unitOfWork.Countries.GetByIdIncludeAsync(request.Id));

            //_validator.ValidateIsNullAndThrowException(countryDto, _localizer, Language.Country);

            return countryDto;
        }
    }
}