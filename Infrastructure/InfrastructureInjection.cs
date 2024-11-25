using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddRepositories();
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlConnectionString");
        return services.AddDbContext<LibraryDbContext>(options =>
            options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies());
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}