using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.DataAccess.Repository
{
    public class BookRepository : BaseRepository<Books, ApplicationDbContext>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Books?> GetBookById(int id)
        {
            return await this.AsQueryable()
                             .Include(i => i.Genres)
                             .Include(i => i.BookTags)
                             .ThenInclude(i => i.Tags)
                             .Include(i => i.BookImages)
                             .ThenInclude(i => i.Images)
                             .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
