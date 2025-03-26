using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSale.Managerment.Domain.Abstract;
namespace BookSale.Managerment.DataAccess.Repository
{
    internal class BookTagRepository : BaseRepository<BookTags>, IBookTagRepository
    {
        public BookTagRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookTags>> GetAllBookTags()
        {
            return await base.GetAll();
        }
    }
    {
    }
}
