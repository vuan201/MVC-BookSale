using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBookRepository
    {
        Task<IEnumerable<Books>> GetAllBooks();
    }
}