using Application.Dto.Models.Authentication;
using AutoMapper;
using Infrastructure.Services.Interfaces.Authentication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Authentication.Commands.RefreshTokenAuthenticationCommand
{
    public class RefreshTokenAuthenticationHandler(ITokenService _authentication, IMapper _mapper) : IRequestHandler<RefreshTokenAuthenticationUseCase, LoginResponseDto>
    {
        public async Task<LoginResponseDto> Handle(RefreshTokenAuthenticationUseCase request, CancellationToken cancellationToken = default)
        {
            return
                _mapper.Map<LoginResponseDto>(
                    await _authentication
                        .RefreshTokenAsync(request.Token));
        }
    }
}