using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Context.Interceptors;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<EntityAuditableSaveChangesInterceptor>();

            services.AddDbContext<AppDbContext>(opt =>
                //opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                opt.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            ));

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    //options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    //options.Password.RequiredUniqueChars = 1;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings.
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});

            //services
            //.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<AppDbContext>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICurrentUser, CurrentUser>();
            //services
            //    .AddAuthorization()
            //    .AddAuthentication()
            //    .AddBearerToken(IdentityConstants.ApplicationScheme);

            //services
            //    .AddIdentityCore<User>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddApiEndpoints();

            return services;
        }

        //public static IApplicationBuilder UseAuthorizationAndAuthentication(this WebApplication app, IConfiguration configuration)
        //{
        //    app.UseAuthentication();
        //    app.UseAuthorization();

        //    return app;
        //}
    }
}