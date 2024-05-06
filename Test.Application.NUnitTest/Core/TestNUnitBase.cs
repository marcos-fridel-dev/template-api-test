using Application.Data.Configuration;
using Application.Dto.Models.Authentication;
using Application.UseCases.Authentication.Commands.LoginAuthenticationCommand;
using Infrastructure.Persistence.Context;
using Infrastructure.Services.Extensions.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Test.Application.NUnitTest.Core
{
    public class TestNUnitBase
    {

        internal ContextWebApp _contextWebApp;
        internal string _url = "";

        protected async Task<HttpClient> GetClient(bool enabledToken = false) 
        {
            var client = _contextWebApp.CreateClient();
            if (enabledToken)
            {
                using var scope = _contextWebApp.Services.CreateScope();
                var config = scope.ServiceProvider.GetService<IConfiguration>();

                var env = config.GetEnvironmentSettings();

                LoginRequestDto loginDto = new LoginRequestDto()
                {
                    UserName = env.Security.UserDefault,
                    Password = env.Security.PasswordDefault
                };
                LoginResponseDto loginResponse = await SendAsync(new LoginAuthenticationUseCase(loginDto));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            }

             return client;
        }

        [OneTimeTearDown]
        protected void RunAfterAnyTests()
        {
            TestContext.Out.WriteLine("OneTimeTearDown - RunAfterAnyTests");
            _contextWebApp.Dispose();
        }

        [OneTimeSetUp]
        protected void RunBeforeAnyTests()
        {
            TestContext.Out.WriteLine("OneTimeSetUp - RunBeforeAnyTests");
            _contextWebApp = new ContextWebApp();

            EnsureDatabase();
        }
        
        [TearDown]
        public async Task Down()
        {
            TestContext.Out.WriteLine("TearDown - Down");
            await ResetState();
        }

        private void EnsureDatabase()
        {
            TestContext.Out.WriteLine("EnsureDatabase");
            using var scope = _contextWebApp.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();
        }

        private async Task ResetState()
        {
            TestContext.Out.WriteLine("ResetState");
            using var scope = _contextWebApp.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            var config = scope.ServiceProvider.GetService<IConfiguration>();

            var env = config.GetEnvironmentSettings();
            var connectionString = env.Database.ConnectionString;

            var respawner = await Respawner.CreateAsync(connectionString, new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            });

            await respawner.ResetAsync(connectionString);
            
            _contextWebApp.GetHost().StartSeed(env);
            
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //List<Role> roles = new();
            //if (context.Roles.Count() == 0)
            //{
            //    roles = RoleFaker.GenerateData();
            //    context.Roles.AddRange(roles);
            //}
            //else
            //{
            //    roles = await context.Roles.ToListAsync();
            //}

            //List<User> users = new();
            //if (context.Users.Count() == 0)
            //{
            //    users = UserFaker.GenerateData(roles);
            //    context.Users.AddRange(users);
            //}

            //await context.SaveChangesAsync(); ;
        }



        // Atajo para buscar entidades según un criterio
        protected async Task<TEntity?> FirstOrDefaultAsync<TEntity>() where TEntity : class
        {
            using var scope = _contextWebApp.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>();

            return await context.Set<TEntity>().FirstOrDefaultAsync();
        }

        // Atajo para buscar entidades según un criterio
        protected async Task<bool> AddRange<TEntity>(List<TEntity> data) where TEntity : class
        {
            try
            {
                using var scope = _contextWebApp.Services.CreateScope();

                var context = scope.ServiceProvider.GetService<AppDbContext>();

                await context.Set<TEntity>().AddRangeAsync(data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Atajo para ejecutar IRequests con el Mediador
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _contextWebApp.Services.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }
    }
}



//public class TestBase
//{
//    protected ContextWebApplicationFactory Application;

//    //Crea un usuario de prueba según los parámetros
//    public async Task<HttpClient> Start()
//    {
//        using var scope = Application.Services.CreateScope();
//        //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//        //var newUser = new IdentityUser(userName);

//        //await userManager.CreateAsync(newUser, password);

//        //foreach (var role in roles)
//        //{
//        //    await userManager.AddToRoleAsync(newUser, role);
//        //}

//        //var accessToken = await GetAccessToken(userName, password);

//        var client = Application.CreateClient();
//        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//        return client;
//    }

//    // Crea un usuario de prueba según los parámetros
//    //public async Task<(HttpClient Client, string UserId)> CreateRoleUseCaseTest(string name)
//    //{
//    //    using var scope = Application.Services.CreateScope();
//    //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//    //    var newUser = new IdentityUser(userName);

//    //    await userManager.CreateAsync(newUser, password);

//    //    foreach (var role in roles)
//    //    {
//    //        await userManager.AddToRoleAsync(newUser, role);
//    //    }

//    //    var accessToken = await GetAccessToken(userName, password);

//    //    var client = Application.CreateClient();
//    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//    //    return (client, newUser.Id);
//    //}

//    //// Al finalizar cada prueba, se restablece la base de datos
//    //[TearDown]
//    //public async Task Down()
//    //{
//    //    await ResetState();
//    //}

//    //// Crea un HttpClient con un JWT válido para un usuario Admin
//    //public Task<(HttpClient Client, string UserId)> GetClientAsAdmin() =>
//    //    CreateTestUser("user@admin.com", "Pass.W0rd", new string[] { "Admin" });

//    //// Crea un HttpClient con un JWT válido para un usuario predeterminado
//    //public Task<(HttpClient Client, string UserId)> GetClientAsDefaultUserAsync() =>
//    //    CreateTestUser("user@normal.com", "Pass.W0rd", Array.Empty<string>());

//    // Libera recursos al finalizar todas las pruebas
//    [OneTimeTearDown]
//    public void RunAfterAnyTests()
//    {
//        Application.Dispose();
//    }

//    // Inicializa la API y la base de datos antes de comenzar las pruebas
//    [OneTimeSetUp]
//    public void RunBeforeAnyTests()
//    {
//        Application = new ContextWebApplicationFactory();

//        EnsureDatabase();
//    }

//    // Atajo para ejecutar IRequests con el Mediador
//    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
//    {
//        using var scope = Application.Services.CreateScope();

//        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

//        return await mediator.Send(request);
//    }

//    // Atajo para agregar entidades a la base de datos
//    protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
//    {
//        using var scope = Application.Services.CreateScope();

//        var context = scope.ServiceProvider.GetService<AppDbContext>();

//        context.Add(entity);

//        await context.SaveChangesAsync();

//        return entity;
//    }

//    // Atajo para buscar entidades por clave primaria
//    protected async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
//    {
//        using var scope = Application.Services.CreateScope();

//        var context = scope.ServiceProvider.GetService<AppDbContext>();

//        return await context.FindAsync<TEntity>(keyValues);
//    }

//    // Atajo para buscar entidades según un criterio
//    protected async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
//    {
//        using var scope = Application.Services.CreateScope();

//        var context = scope.ServiceProvider.GetService<AppDbContext>();

//        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
//    }

//    // Asegura la creación de la base de datos
//    private void EnsureDatabase()
//    {
//        using var scope = Application.Services.CreateScope();
//        var context = scope.ServiceProvider.GetService<AppDbContext>();

//        context.Database.EnsureCreated();
//    }

//    // Atajo para autenticar a un usuario para las pruebas
//    //private async Task<string> GetAccessToken(string userName, string password)
//    //{
//    //    using var scope = Application.Services.CreateScope();

//    //    var result = await SendAsync(new TokenCommand
//    //    {
//    //        UserName = userName,
//    //        Password = password
//    //    });

//    //    return result.AccessToken;
//    //}

//    // Asegura la limpieza de la base de datos
//    private async Task ResetState()
//    {
//        using var scope = Application.Services.CreateScope();
//        var context = scope.ServiceProvider.GetService<AppDbContext>();

//        context.Database.EnsureDeleted();
//        context.Database.EnsureCreated();

//        //await scope;
//    }
//}