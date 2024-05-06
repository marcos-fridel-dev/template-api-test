using Application.Dto.Models.Authentication;
using MediatR;

namespace Application.UseCases.Authentication.Commands.RefreshTokenAuthenticationCommand
{
    public sealed class RefreshTokenAuthenticationUseCase(string _token) : IRequest<LoginResponseDto>
    {
        internal string Token => _token;
    }
}