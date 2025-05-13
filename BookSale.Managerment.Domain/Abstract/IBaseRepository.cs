using System.Linq.Expressions;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBaseRepository<TEntity> : IBaseRepositoryReadonly<TEntity> where TEntity : class
    {
        Task SaveChangesAsync();
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}