using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.City.Queries.GetByStateIdCityQuery
{
    public class GetByStateIdCityHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByStateIdCityUseCase, List<CityDto>>
    {
        public async Task<List<CityDto>> Handle(GetByStateIdCityUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<CityDto>>(await _unitOfWork.Cities.Entity
                .Where(x => x.State.Id == request.StateId && !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync());
        }
    }
}