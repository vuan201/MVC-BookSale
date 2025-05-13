using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.DataAccess.Repository
{
    public class CloudStorageRepository : BaseRepository<CloudStorages, ApplicationDbContext>, ICloudStorageRepository
    {
        public CloudStorageRepository(ApplicationDbContext context) : base(context) { }
        public CloudStorages? GetCloudStorageByType(StorageType type)
        {
            return base.Get(i => i.Type == type);
        }
        public async Task<CloudStorages?> GetCloudStorageByTypeAsync(StorageType type)
        {
            return await base.GetAsync(i => i.Type == type);
        }
    }
}
