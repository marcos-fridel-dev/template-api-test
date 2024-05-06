using Application.Dto.Models.Location;
using AutoMapper;
using Infrastructure.Persistence.Interfaces.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Location.State.Queries.GetByIdStateQuery
{
    public class GetByIdStateHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetByIdStateUseCase, StateDto?>
    {
        public async Task<StateDto?> Handle(GetByIdStateUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<StateDto>(await _unitOfWork.States.GetByIdIncludeAsync(request.Id));
        }
    }
}