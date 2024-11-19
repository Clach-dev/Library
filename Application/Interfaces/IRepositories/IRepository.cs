using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}