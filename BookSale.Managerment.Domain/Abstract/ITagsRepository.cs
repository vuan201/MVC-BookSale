using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface ITagsRepository
    {
        Task<IEnumerable<Tags>> GetAllTaks();
    }
}