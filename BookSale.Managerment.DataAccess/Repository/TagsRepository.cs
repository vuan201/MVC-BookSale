using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
namespace BookSale.Managerment.DataAccess.Repository
{
    internal class TagsRepository : BaseRepository<Tags>, ITagsRepository
    {
        public TagsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tags>> GetAllTaks()
        {
            return await base.GetAll();
        }
    }
    {
    }
}
