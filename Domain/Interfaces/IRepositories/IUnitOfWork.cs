using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable
{    
    IAuthorRepository Authors { get; }
    
    IBookRepository Books { get; }
    
    IBookImageRepository BookImages { get; }
    
    IGenreRepository Genres { get; }
    
    IRefreshTokenRepository RefreshTokens { get; }
    
    IReservationRepository Reservations { get; }
    
    IReviewRepository Reviews { get; }
    
    IUserRepository Users { get; }
    
    IUserImageRepository UserImages { get; }
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}