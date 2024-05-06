using Infrastructure.Services.Interfaces.Authentication;
using Infrastructure.Services.Models.Environment;
using Infrastructure.Services.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Configuration.Authentication
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, SettingsEnvironment settingsEnvironment)
        {
            services
                .AddHttpContextAccessor()
                .AddAuthorization(opt =>
                {
                    opt.AddPolicy("GodRole", policy => policy.RequireClaim(ClaimTypes.Role, "God"));
                    opt.AddPolicy("TestRole", policy => policy.RequireClaim(ClaimTypes.Role, "Test"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = (context) =>
                        {
                            //var userId = int.Parse(context.Principal.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = (context) =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                if (context.Response.Headers.ContainsKey("Token-Expired"))
                                    context.Response.Headers.Remove("Token-Expired");

                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    options.RequireHttpsMetadata = settingsEnvironment.Jwt.RequireHttpsMetadata;
                    options.SaveToken = settingsEnvironment.Jwt.SaveToken;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = settingsEnvironment.Jwt.ValidateIssuer,
                        ValidateAudience = settingsEnvironment.Jwt.ValidateAudience,
                        ValidateLifetime = settingsEnvironment.Jwt.ValidateLifetime,
                        ValidateIssuerSigningKey = settingsEnvironment.Jwt.ValidateIssuerSigningKey,
                        ValidAudience = settingsEnvironment.Jwt.Audience,
                        ValidIssuer = settingsEnvironment.Jwt.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingsEnvironment.Jwt.SigningKey))
                    };

                });

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ILoginService, LoginService>();

            return services;
        }
    }
}