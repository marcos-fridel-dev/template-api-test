using Application.Dto.Configuration;
using Application.Dto.Shared;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application.Dto.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, string? nameAssambyAutoMapperProfile = null)
        {
            nameAssambyAutoMapperProfile = nameAssambyAutoMapperProfile ?? Assembly.GetExecutingAssembly().GetName().Name ?? Assembly.GetCallingAssembly().GetName().Name;

            if (nameAssambyAutoMapperProfile == null)
            {
                return services;
            }

            List<Type> profiles = Profiles.GetAll(nameAssambyAutoMapperProfile);

            profiles.ForEach(x =>
            {
                Profile? instance = (Profile)Activator.CreateInstance(x);
                if (instance != null)
                    services.AddAutoMapper(instance.GetType());
            });

            return services;
        }
    }
}