using AutoMapper;
using CaWorkshop.Application.Common.Mappings;
using System.Reflection;

namespace CaWorkshop.Application.UnitTests;

public static class MapperFactory
{
    public static IMapper Create()
    {
        var configurationProvider = new MapperConfiguration(config =>
        {
            config.AddMaps(Assembly.GetAssembly(typeof(MappingProfile)));
        });

        return configurationProvider.CreateMapper();
    }
}
