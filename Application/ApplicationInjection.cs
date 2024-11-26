using System.Reflection;
using Application.Algorithms;
using Application.Interfaces.IAlgorithms;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMapper()
            .AddMediatR()
            .AddAlgorithms();
    
    private static IServiceCollection AddMapper(this IServiceCollection services)
        => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    private static IServiceCollection AddMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

    private static IServiceCollection AddAlgorithms(this IServiceCollection services)
        => services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<TokensGenerator>();
}