using BookSale.Managerment.DataAccess.Repository;
using BookSale.Managerment.Domain.Entity;

using BookSale.Managerment.Domain.Abstract;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class BookImageRepository : BaseRepository<BookImages, ApplicationDbContext>, IBookImageRepository
    {
        public BookImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
