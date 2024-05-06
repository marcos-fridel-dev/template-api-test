using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Dto.Extensions
{
    public static class MapperExtension
    {
        public static IMapper Create(this IMapper mapper, string? nameAssambyAutoMapperProfile = null)
        {
            nameAssambyAutoMapperProfile = nameAssambyAutoMapperProfile ?? Assembly.GetExecutingAssembly().GetName().Name ?? Assembly.GetCallingAssembly().GetName().Name;

            if (nameAssambyAutoMapperProfile == null)
            {
                return mapper;
            }

            List<Type> automapperProfiles = Assembly.Load(nameAssambyAutoMapperProfile)
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Profile)) && !x.ContainsGenericParameters)
            .ToList();

            MapperConfiguration mapperCreate = new MapperConfiguration(cfg => {
                automapperProfiles.ForEach(x =>
                {
                    Profile? instance = (Profile)Activator.CreateInstance(x);
                    if (instance != null)
                        cfg.AddProfile(instance.GetType());
                });
            });

            return mapperCreate.CreateMapper();
        }
    }
}
