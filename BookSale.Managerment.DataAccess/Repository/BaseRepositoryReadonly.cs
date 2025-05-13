using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.DataAccess.Repository
{
    public class BaseRepositoryReadonly<TEntity, TContext> : IBaseRepositoryReadonly<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext _context;
        public BaseRepositoryReadonly(TContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression is null)
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            return await _context.Set<TEntity>().Where(expression).ToListAsync();
        }
        public TEntity? Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression);
        }
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }
    }
}
