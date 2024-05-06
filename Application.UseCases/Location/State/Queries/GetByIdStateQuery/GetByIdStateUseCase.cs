using Application.Dto.Models.Location;
using MediatR;
using System;

namespace Application.UseCases.Location.State.Queries.GetByIdStateQuery
{
    public sealed class GetByIdStateUseCase(Guid _id) : IRequest<StateDto?>
    {
        internal Guid Id => _id;
    }
}