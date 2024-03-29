﻿using DataAccessLayer.Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Repository.DependencyInject;

public static class RepositoryProvider
{
    public static void RepositoriesProvide(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IFarmRepository, FarmRepository>();
        services.AddTransient<IInnogotchiRepository, InnogotchiRepository>();
        services.AddTransient<IUserFriendRepository, UserFriendRepository>();
        services.AddTransient<IInnogotchiStateRepository, InnogotchiStateRepository>();
    }
}
