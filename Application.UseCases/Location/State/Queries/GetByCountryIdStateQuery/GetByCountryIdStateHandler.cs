using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.State.Queries.GetByCountryIdStateQuery
{
    public class GetByCountryIdStateHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByCountryIdStateUseCase, List<StateDto>>
    {
        public async Task<List<StateDto>> Handle(GetByCountryIdStateUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<StateDto>>(await _unitOfWork.States.Entity
                .Where(x => x.Country.Id == request.CountryId && !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync());
        }
    }
}