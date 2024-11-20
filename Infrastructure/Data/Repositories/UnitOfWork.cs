using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    private readonly Dictionary<Type, Func<LibraryDbContext, object>> _repositoryFactories;

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
        _repositoryFactories = new Dictionary<Type, Func<LibraryDbContext, object>>
        {
            { typeof(Author), ctx => new AuthorRepository(ctx) },
            { typeof(Book), ctx => new BookRepository(ctx) },
            { typeof(Genre), ctx => new GenreRepository(ctx) },
            { typeof(Reservation), ctx => new ReservationRepository(ctx) },
            { typeof(User), ctx => new UserRepository(ctx) },
        };
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositoryFactories.TryGetValue(typeof(TEntity), out var factory))
        {
            var repository = (IRepository<TEntity>)factory.Invoke(_context);
            return repository;
        }
        else
            throw new InvalidOperationException($"No repository factory found for type {typeof(TEntity).Name}.");
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}