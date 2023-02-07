using Core.Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Domain.Services.DependencyInject;

public static class ServiceProvider
{
    public static void ServicesProvide(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IFarmService, FarmService>();
        services.AddTransient<IInnogotchiService, InnogotchiService>();
        services.AddTransient<IInnogotchiStateService, InnogotchiStateService>();
        services.AddTransient<InnogotchiStateService>();
        services.AddSingleton<PeriodicHostedService>();
    }
}