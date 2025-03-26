using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface IBookTagRepository
    {
        Task<IEnumerable<BookTags>> GetAllBookTags();
    }
}