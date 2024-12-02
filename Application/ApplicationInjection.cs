using System.Reflection;
using Application.Common.Algorithms;
using Application.Common.Interfaces.IAlgorithms;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMapper()
            .AddMediatR()
            .AddAlgorithms();
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }

    private static IServiceCollection AddAlgorithms(this IServiceCollection services)
    {
        return services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<TokensGenerator>();
    }
}