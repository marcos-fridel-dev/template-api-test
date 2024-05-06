using Infrastructure.Services.Interfaces.Mail;
using Infrastructure.Services.Models.Environment;
using Infrastructure.Services.Services.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Configuration.Mail
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMail(this IServiceCollection services, SettingsEnvironment env)
        {
            services.AddTransient<IMailService, SmtpMailService>();

            return services;
        }
    }
}