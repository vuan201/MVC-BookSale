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

        public async Task<Tags?> GetByIdAsync(int id) => await this.GetAsync(i => i.Id == id);
        public async Task<List<Tags>?> GetAllTag()
        {
            var result = await GetListAsync();
            return result.ToList();
        }
    }
}
