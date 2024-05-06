using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.User.Queries.GetByIdUserQuery
{
    public sealed class GetByIdUserUseCase(Guid _id) : IRequest<UserDto?>
    {
        internal Guid Id => _id;
    }
}