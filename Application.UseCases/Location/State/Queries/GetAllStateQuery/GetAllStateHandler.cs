using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.State.Queries.GetAllStateQuery
{
    public class GetAllStateHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllStateUseCase, IEnumerable<StateDto>>
    {

        public async Task<IEnumerable<StateDto>> Handle(GetAllStateUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<StateDto>>(
                await _unitOfWork.States
                    .GetAllAsync(request.PageNumber, request.PageSize, request.IsDeleted));
        }
    }
}