using System.Linq.Expressions;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBaseRepositoryReadonly<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
        TEntity? Get(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}