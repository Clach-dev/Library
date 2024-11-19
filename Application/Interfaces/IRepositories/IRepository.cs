using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    void DeleteAsync(TEntity entity);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}