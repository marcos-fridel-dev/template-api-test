using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.City.Queries.GetByIdCityQuery
{
    public class GetByIdCityHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdCityUseCase, CityDto?>
    {
        public async Task<CityDto?> Handle(GetByIdCityUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<CityDto>(await _unitOfWork.Cities.GetByIdIncludeAsync(request.Id));
        }
    }
}