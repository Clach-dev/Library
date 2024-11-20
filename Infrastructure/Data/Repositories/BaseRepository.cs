using System.Linq.Expressions;
using Application.DTOs;
using Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public abstract class BaseRepository<TEntity>: IRepository<TEntity> where TEntity : class
{
    protected readonly LibraryDbContext _context;
    protected readonly DbSet<TEntity> _entities;
    
    public BaseRepository(LibraryDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public async Task<(IEnumerable<TEntity>, int)> GetAllAsync(PageInfo pageInfo, CancellationToken cancellationToken = default)
    {
        var entities = await _entities
            .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
            .Take(pageInfo.PageSize)
            .ToListAsync(cancellationToken);
        
        var totalCount = await _entities.CountAsync();

        return (entities, totalCount);
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _entities.FindAsync(id, cancellationToken);
    
    public async Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _entities.Where(predicate).ToListAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _entities.AddAsync(entity, cancellationToken);
    
    public async Task Delete(TEntity entity)
        => _entities.Remove(entity);
}