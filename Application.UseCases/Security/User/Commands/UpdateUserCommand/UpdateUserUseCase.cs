using Application.Dto.Models.Security;
using MediatR;
using System;

namespace Application.UseCases.Security.User.Commands.UpdateUserCommand
{
    public sealed class UpdateUserUseCase(Guid _id, UserDto _user) : IRequest<UserDto>
    {
        internal Guid Id => _id;
        internal UserDto User => _user;
    }
}