using Application.Common.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork(LibraryDbContext context) : IUnitOfWork
{
    private IAuthorRepository? _authorRepository;
    
    private IBookRepository? _bookRepository;
    
    private IGenreRepository? _genreRepository;
    
    private IReservationRepository? _reservationRepository;
    
    private IUserRepository? _userRepository;

    private IRefreshTokenRepository? _refreshTokenRepository;

    public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(context);

    public IBookRepository Books => _bookRepository ??= new BookRepository(context);
    
    public IGenreRepository Genres => _genreRepository ??= new GenreRepository(context);
    
    public IReservationRepository Reservations  => _reservationRepository ??= new ReservationRepository(context);
    
    public IUserRepository Users => _userRepository ??= new UserRepository(context);

    public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(context);
        
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}