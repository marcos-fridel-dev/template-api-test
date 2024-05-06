using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Services.Exceptions;
using Infrastructure.Services.Interfaces.Authentication;
using Infrastructure.Services.Models.Authentication;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;

namespace Infrastructure.Services.Services.Authentication
{
    internal class LoginService(IUnitOfWork _unitOfWork, ITokenService _tokenService, IStringLocalizer _localizer) : ILoginService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest login)
        {
            if (login.UserName.IsNullOrEmpty() || login.Password.IsNullOrEmpty())
                throw new BadRequestException(_localizer);

            //User user = await _service.Login(login.User, login.Password);

            User? user = await _unitOfWork.Users.FindByNameAsync(login.UserName);

            if (user == null)
            {
                throw new ForbidException(_localizer, Language.UserOrPasswordNotValid);
            }

            if (!await _unitOfWork.Login.CheckPasswordAsync(user, login.Password))
            {
                throw new ForbidException(_localizer, Language.UserOrPasswordNotValid);
            }

            string token = await _tokenService.GetToken(user);

            return new LoginResponse()
            {
                Token = token,
                User = user,
            };
        }
    }
}