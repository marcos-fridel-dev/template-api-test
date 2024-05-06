using Infrastructure.Services.Models.Authentication;

namespace Infrastructure.Services.Interfaces.Authentication
{
    public interface ILoginService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest login);
    }
}