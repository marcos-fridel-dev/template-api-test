using Domain.Models.Security;
using Infrastructure.Services.Models.Authentication;

namespace Infrastructure.Services.Interfaces.Authentication
{
    public interface ITokenService
    {
        public Task<string> GetToken(User user);
        public Task<LoginResponse> RefreshTokenAsync(string token);
    }
}