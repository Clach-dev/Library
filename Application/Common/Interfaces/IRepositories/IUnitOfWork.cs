namespace Application.Common.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable
{    
    IUserRepository Users { get; }
    
    IAuthorRepository Authors { get; }

    IBookRepository Books { get; }
    
    IGenreRepository Genres { get; }
    
    IReservationRepository Reservations { get; }
    
    IRefreshTokenRepository RefreshTokens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}