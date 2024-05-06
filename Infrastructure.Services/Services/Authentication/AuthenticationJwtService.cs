using Domain.Models.Security;
using Infrastructure.Persistence.Enums.User;
using Infrastructure.Services.Exceptions;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Interfaces.Authentication;
using Infrastructure.Services.Models.Authentication;
using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Shared.Localization.Resources.Languages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Services.Authentication
{
    //public class AuthenticationJwtService(IConfiguration _configuration, UserManager<User> _userManager, IStringLocalizer localizer) : IAuthenticationService
    public class AuthenticationJwtService(IConfiguration _configuration, UserManager<User> _userManager, IStringLocalizer localizer) : IAuthenticationService
    {
        public async Task<string> GetToken(User user)
        {
            EnvironmentSettings env = _configuration.GetEnvironmentSettings();

            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, env.Jwt.Subject),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixTimeSeconds().ToString()),
                //new Claim(UserClaim.UserId.GetDescription(), user.Id.ToString()),
                //new Claim(UserClaim.CompanyId.GetDescription(), user.CompanyId.ToString()),
                //new Claim(UserClaim.CompanySlug.GetDescription(), companySlug),
                //new Claim(ClaimTypes.Role, "God"),
                new Claim(Claims.UserId.ToString(), user.Id.ToString()),
                new Claim(Claims.UserName.ToString(), user.UserName),
                new Claim(Claims.FirstName.ToString(), user.FirstName),
                new Claim(Claims.LastName.ToString(), user.LastName),
                ////new Claim("LoginCompany", user.Company.Slug),
                new Claim(Claims.Email.ToString(), user.Email),
                //new Claim("CompanyId", user.CompanyId.ToString()),
            };

            foreach (var rol in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            DateTime dateTimeExpires = DateTime.UtcNow.AddMinutes(env.Jwt.ExpiresMinutes);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(env.Jwt.SigningKey));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: env.Jwt.Issuer,
                audience: env.Jwt.Audience,
                claims: claims,
                expires: dateTimeExpires,
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest login)
        {
            if (login.UserName.IsNullOrEmpty() || login.Password.IsNullOrEmpty())
                throw new BadRequestException(localizer);

            //User user = await _service.Login(login.User, login.Password);

            User? user = await _userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                throw new ForbidException(localizer, Language.UserOrPasswordNotValid);
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new ForbidException(localizer, Language.UserOrPasswordNotValid);
            }

            string token = await GetToken(user);

            return new LoginResponse()
            {
                Token = token,
                User = user,
            };
        }
        public async Task<LoginResponse> RefreshTokenAsync(string token)
        {
            EnvironmentSettings env = _configuration.GetEnvironmentSettings();

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(env.Jwt.SigningKey));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateIssuer = env.Jwt.ValidateIssuer,
                ValidateAudience = env.Jwt.ValidateAudience,
                ValidateLifetime = env.Jwt.ValidateLifetime,
                ValidateIssuerSigningKey = env.Jwt.ValidateIssuerSigningKey,
                ValidAudience = env.Jwt.Audience,
                ValidIssuer = env.Jwt.Issuer,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken tokenData = (JwtSecurityToken)validatedToken;

            string? userName = tokenData.Claims.FirstOrDefault(x => x.Type == Claims.UserName.ToString())?.Value;

            if (userName == null)
            {
                throw new ForbidException(localizer, String.Format(Language.NoItemInformationFoundInTheToken, Language.User.ToLower()));
            }

            User? user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new ForbidException(localizer, String.Format(Language.NoItemInformationFoundInTheToken, Language.User.ToLower()));
            }

            token = await GetToken(user);

            return new LoginResponse()
            {
                Token = token,
                User = user,
            };
        }
    }
}
