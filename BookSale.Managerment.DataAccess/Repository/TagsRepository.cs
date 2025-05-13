using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
namespace BookSale.Managerment.DataAccess.Repository
{
    internal class TagsRepository : BaseRepository<Tags, ApplicationDbContext>, ITagsRepository
    {
        public TagsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
