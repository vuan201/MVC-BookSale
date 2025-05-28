using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface ITagsRepository : IBaseRepository<Tags>
    {
        Task<Tags?> GetByIdAsync(int id);
    }
}