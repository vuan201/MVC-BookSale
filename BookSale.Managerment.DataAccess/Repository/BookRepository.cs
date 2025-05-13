using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
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
    }
}
