using System.Linq.Expressions;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBaseRepositoryReadonly<TEntity> where TEntity : class
    {
        TEntity? Get(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}