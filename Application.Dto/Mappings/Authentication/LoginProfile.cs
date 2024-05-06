using Application.Dto.Models.Authentication;
using Infrastructure.Services.Models.Authentication;

namespace Application.Dto.Mappings.Authentication
{
    public class LoginRequestProfile : DefaultProfile<LoginRequest, LoginRequestDto> { }
    public class LoginResponseProfile : DefaultProfile<LoginResponse, LoginResponseDto> { }
}