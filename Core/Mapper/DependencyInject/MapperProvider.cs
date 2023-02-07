using AutoMapper;
using Mapping.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Mapping.DependencyInject;

public static class MapperProvider
{
    public static void MappersProvide(this IServiceCollection services)
    {
        var configuration = new MapperConfiguration(options =>
        {
            options.AddProfile(new FarmMapper());
            options.AddProfile(new InnogotchiMapper());
            options.AddProfile(new InnogotchiStateMapper());
            options.AddProfile(new UserMapper());
            options.AddProfile(new MediaMapper());
        });

        var mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);
    }
}
