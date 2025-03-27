using BookSale.Managerment.DataAccess.Repository;
using BookSale.Managerment.Domain.Entity;

using BookSale.Managerment.Domain.Abstract;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class BookImageRepository : BaseRepository<BookImages>, IBookImageRepository
    {
        public BookImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookImages>> GetAllBookImages()
        {
            return await base.GetAll();
        }
    }
}
