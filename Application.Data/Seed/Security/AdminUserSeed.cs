using Application.Data.Interfaces;
using Domain.Models.Security;
using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Services.Models.Environment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Application.Data.Seed.Security
{
    public class AdminUserSeed(IServiceScope serviceScope, SettingsEnvironment env) : IServiceScopeSeed
    {
        public async Task<bool> Process()
        {
            try
            {
                string roleDefault = env.Security.RoleDefault;
                //bool existsRole = await ExistsRoleDefaultSeedAsync(roleDefault);
                //if (roleDefault != null)
                await UserDefaultSeedAsync(roleDefault);
                //await UserDefaultSeedAsync("admin");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //private async Task<bool> ExistsRoleDefaultSeedAsync(string roleDefault)
        //{
        //    try
        //    {
        //        RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        IdentityRole? role = await roleManager.Roles
        //            .Where(x => x.Name == roleDefault)
        //            .FirstOrDefaultAsync();

        //        if (role != null)
        //        {
        //            return true;
        //        }

        //        await roleManager.CreateAsync(new IdentityRole
        //        {
        //            Name = roleDefault
        //        });

        //        return true;
        //    }
        //    catch(Exception ex) 
        //    {
        //        return false;
        //    }
        //}

        private async Task<bool> UserDefaultSeedAsync(string roleDefault)
        {
            try
            {
                string userDefault = env.Security.UserDefault;
                string firstNameDefault = env.Security.FirstNameDefault;
                string lastNameDefault = env.Security.LastNameDefault;
                string emailDefault = env.Security.EMailDefault;
                string passwordDefault = env.Security.PasswordDefault;

                IUnitOfWork unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                User? user = await unitOfWork.Users
                    .FirstOrDefaultAsync(x => x.UserName == userDefault);

                Role? role = await unitOfWork.Roles
                    .FirstOrDefaultAsync(x => x.Name == roleDefault);

                if (role == null)
                {
                    role = new Role()
                    {
                        Name = roleDefault
                    };
                }

                if (user != null)
                {
                    return true;
                }

                user = new User
                {
                    UserName = userDefault,
                    FirstName = firstNameDefault,
                    LastName = lastNameDefault,
                    Email = emailDefault,
                    //Password = passwordDefault,
                    Status = Domain.Enums.Security.UserStatus.Activate,
                    Roles = [role]
                };

                unitOfWork.Users.Add(user);

                await unitOfWork.Login.ChangePasswordAsync(user, passwordDefault);

                await unitOfWork.SaveAsync();

                //await userManager.AddToRoleAsync(user, roleDefault);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}