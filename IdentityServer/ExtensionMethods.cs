using DataAccessLayer.Data;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer;

public static class ExtensionMethods
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabaseConnection"),
                migration => migration.MigrationsAssembly(migrationAssembly));
        });

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer(options =>
        {
            options.UserInteraction.LoginUrl = null;
        })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = context => context.UseSqlServer(
                    configuration.GetConnectionString("IdentityDatabaseConnection"),
                    migration => migration.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = context => context.UseSqlServer(
                    configuration.GetConnectionString("IdentityDatabaseConnection"),
                    migration => migration.MigrationsAssembly(migrationAssembly));
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<User>();

    }
}