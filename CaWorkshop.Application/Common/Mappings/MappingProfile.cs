﻿using System.Reflection;

using AutoMapper;

namespace CaWorkshop.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var method = type.GetMethod("Mapping") ??
                         type.GetInterface("IMapFrom`1")?
                            .GetMethod("Mapping");

            var instance = Activator.CreateInstance(type);

            method?.Invoke(instance, new object[] { this });
        }
    }
}
