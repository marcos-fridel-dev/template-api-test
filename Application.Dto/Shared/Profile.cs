using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Dto.Shared
{
    public class Profiles
    {
        public static List<Type> GetAll(string? nameAssambyAutoMapperProfile = null)
        {
            nameAssambyAutoMapperProfile = nameAssambyAutoMapperProfile ?? Assembly.GetExecutingAssembly().GetName().Name ?? Assembly.GetCallingAssembly().GetName().Name;

            if (nameAssambyAutoMapperProfile == null)
            {
                return new List<Type>();
            }

            return Assembly.Load(nameAssambyAutoMapperProfile)
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Profile)) && !x.ContainsGenericParameters)
                .ToList();
        }
    }
}