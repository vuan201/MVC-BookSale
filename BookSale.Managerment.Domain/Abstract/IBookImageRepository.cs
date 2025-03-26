using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBookImageRepository
    {
        Task<IEnumerable<BookImages>> GetAllBookImages();
    }
}