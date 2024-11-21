﻿using System.Linq.Expressions;
using Application.Dtos;
using Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public abstract class BaseRepository<TEntity>: IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _entities;
    
    protected BaseRepository(LibraryDbContext context)
    {
        _entities = context.Set<TEntity>();
    }
    
    public async Task<(IEnumerable<TEntity>, int)> GetAllAsync(PageInfo pageInfo, CancellationToken cancellationToken = default)
    {
        var entities = await _entities
            .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
            .Take(pageInfo.PageSize)
            .ToListAsync(cancellationToken);
        
        var totalCount = await _entities.CountAsync(cancellationToken);

        return (entities, totalCount);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _entities.FindAsync(id, cancellationToken);
    
    public async Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _entities.Where(predicate).ToListAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _entities.AddAsync(entity, cancellationToken);
    
    public async Task Delete(TEntity entity)
        => _entities.Remove(entity);
}