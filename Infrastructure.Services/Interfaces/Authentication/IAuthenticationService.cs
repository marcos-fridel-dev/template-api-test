using Infrastructure.Services.Models.Authentication;

namespace Infrastructure.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest login);
        public Task<LoginResponse> RefreshTokenAsync(string token);
    }
}