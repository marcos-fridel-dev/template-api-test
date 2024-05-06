using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.City.Queries.GetAllCityQuery
{
    public class GetAllCityHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllCityUseCase, IEnumerable<CityDto>>
    {
        public async Task<IEnumerable<CityDto>> Handle(GetAllCityUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<CityDto>>(
                await _unitOfWork.Cities
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}