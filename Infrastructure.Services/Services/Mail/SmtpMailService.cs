using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Services.Interfaces.Mail;
using Infrastructure.Services.Models.Environment;
using Infrastructure.Services.Models.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Npgsql.TypeMapping;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Services.Services.Mail
{
    //mlsn.7661a570a0cb395a666fc1c703d8b968e982a1212c66f984f8646082b302b862

    //smtp.mailersend.net
    //587
    //TLS

    //MS_LfJ5I7@trial-0r83ql3j1xvgzw1j.mlsender.net
    //FAxzUqe23aro55X9
    public class SmtpMailService(ICurrentUser _currentUser, IUnitOfWork _unitOfWork, SettingsEnvironment _settings) : IMailService
    {

        public async Task<SendMailResponse> SendAsync(
            string subject, string body,
            string to, string? cc = "", string? bcc = "")
        {

            User user = await _unitOfWork.Users
                .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId);

            return await SendAsync(
                user,
                subject, body,
                to, cc, bcc);
        }

        public async Task<SendMailResponse> SendAsync(
            User user,
            string subject, string body,
            string to, string? cc = "", string? bcc = "")
        {
            string from = $"{user.UserName.ToLower()}";

            if (_settings.Mail.Smtp.UseDomainUserNameToFrom) 
            {
                string domain = _settings.Mail.Smtp.UserName.Split("@").Last();
                from = $"{from}@{domain}";
            }

            string fromDisplay = $"{user.LastName}, {user.FirstName}";

            return await SendAsync(
                _settings.Mail.Smtp.Host,
                _settings.Mail.Smtp.Port,
                _settings.Mail.Smtp.UserName,
                _settings.Mail.Smtp.Password,
                _settings.Mail.Smtp.EnabledSsl,
                from, fromDisplay,
                subject, body,
                to, cc, bcc);
        }

        public async Task<SendMailResponse> SendAsync(
            string from, string fromDisplay,
            string subject, string body,
            string to, string? cc = "", string? bcc = "")
        {
            return await SendAsync(
                _settings.Mail.Smtp.Host,
                _settings.Mail.Smtp.Port,
                _settings.Mail.Smtp.UserName,
                _settings.Mail.Smtp.Password,
                _settings.Mail.Smtp.EnabledSsl,
                from, fromDisplay,
                subject, body,
                to, cc, bcc);
        }

        protected async Task<SendMailResponse> SendAsync(
            string hostSmtp, int port, string userName, string password, bool enabledSsl,
            string from, string fromDisplay,
            string subject, string body,
            string to, string? cc = "", string? bcc = "")
        {
            try
            {
                SmtpClient client = new SmtpClient(hostSmtp);
                client.Port = port;
                client.EnableSsl = enabledSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                
                client.UseDefaultCredentials = false;
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(userName, password);
                client.Credentials = basicAuthenticationInfo;

                MailAddress mailFrom = new MailAddress(from, fromDisplay);

                MailMessage mail = new();//, mailTo);

                mail.From = mailFrom;

                foreach (string m in to.Split(";"))
                {
                    mail.To.Add(m);
                }

                if (!cc.IsNullOrEmpty())
                {
                    foreach (string m in cc.Split(";"))
                    {
                        mail.CC.Add(m);
                    }
                }
                if (!bcc.IsNullOrEmpty())
                {
                    foreach (string m in bcc.Split(";"))
                    {
                        mail.Bcc.Add(m);
                    }
                }

                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;

                // set body-message and encoding
                mail.Body = body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                await client.SendMailAsync(mail);

                return new() {
                    Success = true,
                    From = from,
                    FromDisplay = fromDisplay,
                    Subject = subject,
                    To = to,
                    Cc = cc,
                    Bcc = bcc
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Success = false,
                    Message = ex.Message.ToString(),
                    From = from,
                    FromDisplay = fromDisplay,
                    Subject = subject,
                    To = to,
                    Cc = cc,
                    Bcc = bcc
                };
            }
        }
    }
}