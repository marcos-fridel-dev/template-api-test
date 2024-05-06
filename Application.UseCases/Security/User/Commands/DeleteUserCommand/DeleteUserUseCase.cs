using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.User.Commands.DeleteUserCommand
{
    public sealed class DeleteUserUseCase(Guid _id) : IRequest<UserDto>
    {
        internal Guid Id => _id;
    }
}