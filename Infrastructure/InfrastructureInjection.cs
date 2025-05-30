using Domain.Interfaces.IAlgorithms;
using Domain.Interfaces.IRepositories;
using Infrastructure.Algorithms;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddStorage(configuration)
            .AddRepositories()
            .AddAlgorithms();
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlConnectionString");
        
        return services.AddDbContext<LibraryDbContext>(options =>
            options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies());
    }

    private static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(configuration.GetConnectionString("AzureConnectionString"));
        });

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IBookImageRepository, BookImageRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserImageRepository, UserImageRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    
    private static IServiceCollection AddAlgorithms(this IServiceCollection services)
    {
        return services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokensGenerator, TokensGenerator>();
    }
}