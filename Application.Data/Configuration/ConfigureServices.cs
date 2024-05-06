using Application.Data.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Interfaces.Environment;
using Infrastructure.Services.Models.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Data.Configuration
{
    public static class ConfigureServices
    {
        public static void StartSeed(this IHost host, SettingsEnvironment settingsEnvironment)
        { 
            StartSeedAsync(host, settingsEnvironment)
                .Wait();
        }

        public static async Task StartSeedAsync(this IHost host, SettingsEnvironment settingsEnvironment)
        {
            try
            {
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    //context.Database.EnsureCreated();
                    await context.Database.MigrateAsync();

                    try
                    {
                        IEnumerable<Type> typesServiceScopeSeed = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => typeof(IServiceScopeSeed).IsAssignableFrom(p));

                        foreach (var type in typesServiceScopeSeed)
                        {
                            if (!type.IsInterface)
                            {
                                IServiceScopeSeed? seed = (IServiceScopeSeed)Activator.CreateInstance(type, scope, settingsEnvironment);
                                await seed.Process();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    try
                    {
                        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        IEnumerable<Type> typesUnitOfWork = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => typeof(IUnitOfWorkSeed).IsAssignableFrom(p) && !p.IsInterface);

                        foreach (var type in typesUnitOfWork)
                        {
                            if (!type.IsInterface)
                            {
                                IUnitOfWorkSeed? seed = (IUnitOfWorkSeed)Activator.CreateInstance(type, unitOfWork);
                                await seed.Process();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}