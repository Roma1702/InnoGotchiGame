using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mapping.DependencyInject;

public static class MapperProvider
{
    public static void MappersProvide(this IServiceCollection services)
    {
        var configuration = new MapperConfiguration(options =>
        {
            options.AddMaps(Assembly.GetExecutingAssembly());
        });

        var mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);
    }
}
