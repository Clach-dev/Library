using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork(LibraryDbContext context, BlobServiceClient blobServiceClient, IConfiguration configuration) : IUnitOfWork
{
    private IAuthorRepository? _authorRepository;
    
    private IBookRepository? _bookRepository;
    
    private IBookImageRepository? _bookImageRepository;
    
    private IGenreRepository? _genreRepository;
    
    private IRefreshTokenRepository? _refreshTokenRepository;
    
    private IReservationRepository? _reservationRepository;
    
    private IReviewRepository? _reviewRepository;

    private IUserRepository? _userRepository;

    private IUserImageRepository? _userImageRepository;

    public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(context);

    public IBookRepository Books => _bookRepository ??= new BookRepository(context);
    
    public IBookImageRepository BookImages => _bookImageRepository ??= new BookImageRepository(blobServiceClient, configuration);
    
    public IGenreRepository Genres => _genreRepository ??= new GenreRepository(context);
    
    public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(context);
    
    public IReservationRepository Reservations  => _reservationRepository ??= new ReservationRepository(context);
    
    public IReviewRepository Reviews => _reviewRepository ??= new ReviewRepository(context);

    public IUserRepository Users => _userRepository ??= new UserRepository(context);

    public IUserImageRepository UserImages => _userImageRepository ??= new UserImageRepository(blobServiceClient, configuration);
    
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}