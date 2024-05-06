using Application.Dto.Models.Authentication;
using MediatR;

namespace Application.UseCases.Authentication.Commands.LoginAuthenticationCommand
{
    public sealed class LoginAuthenticationUseCase(LoginRequestDto _login) : IRequest<LoginResponseDto>
    {
        internal LoginRequestDto Login => _login;
    }
}