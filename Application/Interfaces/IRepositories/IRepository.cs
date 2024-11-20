using System.Linq.Expressions;
using Application.DTOs;

namespace Application.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<(IEnumerable<TEntity>, int)> GetAllAsync(PageInfo pageInfo, CancellationToken cancellationToken);
    
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task Delete(TEntity entity);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}