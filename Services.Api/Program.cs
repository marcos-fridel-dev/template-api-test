using Application.Data.Configuration;
using Application.Dto.Configuration;
using Application.Services.Configuration;
using Application.UseCases.Configuration;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Extensions.Configuration;
using Infrastructure.Services.Models.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Api.Configuration;
using Services.Api.Configuration.Middleware;
using Services.Api.Models.Middleware.Results;
using Shared.Localization.Configuration;
using System;
using System.IO;
using System.Text.Json.Serialization;

//var env = Environment.GetEnvironmentVariable("TEST");
//Console.WriteLine(env);
//var builderDb = new DbContextOptionsBuilder<AppDbContext>()
//    .UseInMemoryDatabase($"CqrsDbContext-{Guid.NewGuid()}");

//HttpContextAccessor _httpContextAccessor = (new Mock<HttpContextAccessor>()).Object;
//CurrentUser currentUser = (new Mock<CurrentUser>(_httpContextAccessor)).Object;

//EntityAuditableSaveChangesInterceptor entityAuditableSaveChangesInterceptor = new(currentUser);

//DbContextOptions<AppDbContext> options = builderDb.Options;
//var _context = new AppDbContext(options, entityAuditableSaveChangesInterceptor);

//UnitOfWork _unitOfWork = new(_context);

//_context.Set<Role>().AddRange(RoleFaker.GenerateData());
//_context.SaveChanges();

//var list = _context.Set<Role>().ToList();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var config = new ConfigurationBuilder()
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, true)
    .AddEnvironmentVariables()
    .Build();

builder.Services
    .AddControllers(opt =>  // or AddMvc()
    {
        // remove formatter that turns nulls into 204 - No Content responses
        // this formatter breaks Angular's Http response JSON parsing
        opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
        opt.OutputFormatters.RemoveType<StringOutputFormatter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services
    .Configure<ApiBehaviorOptions>(x => x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult());

SettingsEnvironment env = builder.Configuration.GetEnvironmentSettings();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSettingsEnvironmentServices(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddHttpServices(builder.Configuration);
builder.Services.AddInfrastructurePersistence(env.Database.ConnectionString);
builder.Services.AddInfrastructureServices(env);
builder.Services.AddLocalizer();
builder.Services.AddMediator();

builder.Services.AddInjections();

builder.Services.AddLogging();

var app = builder.Build();


//app.UseHttpsRedirection();

app.UseLocalizer();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddlewares();

app.UseInfrastructureServices(env);

app.MapControllers();

app.StartSeed(env);

app.Run();
