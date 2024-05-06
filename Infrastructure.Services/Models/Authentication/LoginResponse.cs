using Domain.Models.Security;

namespace Infrastructure.Services.Models.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}