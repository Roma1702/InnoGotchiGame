using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Models.Core;
using Validation.Validators;

namespace Validation.DependencyInject;

public static class ValidatorsProvider
{
    public static void ValidatorsProvide(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserDto>, UserValidator>();
        services.AddScoped<IValidator<FarmDto>, FarmValidator>();
        services.AddScoped<IValidator<InnogotchiDto>, InnogotchiValidator>();
        services.AddScoped<IValidator<InnogotchiStateDto>, InnogotchiStateValidator>();
        services.AddScoped<IValidator<ChangePasswordDto>, ChangeUserPasswordValidator>();
        services.AddScoped<IValidator<MediaDto>, MediaValidator>();
        services.AddScoped<IValidator<ShortUserDto>, ShortUserValidator>();
    }
}
