using Application.Dto.Models.Authentication;
using AutoMapper;
using Infrastructure.Services.Interfaces.Authentication;
using Infrastructure.Services.Models.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Authentication.Commands.LoginAuthenticationCommand
{
    public class LoginAuthenticationHandler(ILoginService _loginService, IMapper _mapper) : IRequestHandler<LoginAuthenticationUseCase, LoginResponseDto>
    {
        public async Task<LoginResponseDto> Handle(LoginAuthenticationUseCase request, CancellationToken cancellationToken = default)
        {
            return
                _mapper.Map<LoginResponseDto>(
                    await _loginService
                        .LoginAsync(_mapper.Map<LoginRequest>(request.Login)));
        }
    }
}