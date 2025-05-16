using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IGenreRepository : IBaseRepository<Genres>
    {
        Task<Genres?> GetByIdAsync(int id);
    }
}
