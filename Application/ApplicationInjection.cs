﻿using System.Reflection;
using Application.Alghorithms;
using Application.Interfaces.IAlghoritms;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMapper()
            .AddMediatR()
            .AddAlghoritms();
    
    private static IServiceCollection AddMapper(this IServiceCollection services)
        => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    private static IServiceCollection AddMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

    private static IServiceCollection AddAlghoritms(this IServiceCollection services)
        => services.AddScoped<IPasswordHasher, PasswordHasher>();
}