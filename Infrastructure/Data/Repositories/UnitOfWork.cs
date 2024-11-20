using Application.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    
    private IAuthorRepository _authorRepository;
    
    private IBookRepository _bookRepository;
    
    private IGenreRepository _genreRepository;
    
    private IReservationRepository _reservationRepository;
    
    private IUserRepository _userRepository;

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
    }

    public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);

    public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
    
    public IGenreRepository Genres => _genreRepository ??= new GenreRepository(_context);
    
    public IReservationRepository Reservations  => _reservationRepository ??= new ReservationRepository(_context);
    
    public IUserRepository Users => _userRepository ??= new UserRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}