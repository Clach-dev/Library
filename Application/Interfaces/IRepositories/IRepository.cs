using System.Linq.Expressions;
using Application.Dtos;

namespace Application.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    protected Task<(IEnumerable<TEntity>, int)> GetAllAsync(PageInfo pageInfo, CancellationToken cancellationToken = default);
    
    protected Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    protected Task<IEnumerable<TEntity>> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    
    protected Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    protected Task Delete(TEntity entity);
}