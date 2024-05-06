using Domain.Models.Security;
using Infrastructure.Services.Models.Mail;

namespace Infrastructure.Services.Interfaces.Mail
{
    public interface IMailService
    {
        Task<SendMailResponse> SendAsync(
            string subject, string body,
            string to, string? cc = "", string? bcc = "");

        Task<SendMailResponse> SendAsync(
            User user,
            string subject, string body,
            string to, string? cc = "", string? bcc = "");

        Task<SendMailResponse> SendAsync(
            string from, string fromDisplay,
            string subject, string body,
            string to, string? cc = "", string? bcc = "");
    }
}