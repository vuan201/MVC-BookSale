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
    public class BaseRepository<TEntity, TContext> : BaseRepositoryReadonly<TEntity, TContext>, IBaseRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public BaseRepository(TContext context) : base(context) { }

        // Add
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public async Task CreateAsync(IList<TEntity> entities)
        {
            if (entities != null && entities.Any())
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
            }
        }

        // Update
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        // Delete
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        // Save
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
