using DataAccessLayer.Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Repository.DependencyInject;

public static class RepositoryProvider
{
    public static void Provide(IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IFarmRepository, FarmRepository>();
        services.AddTransient<IInnogotchiRepository, InnogotchiRepository>();
    }
}
