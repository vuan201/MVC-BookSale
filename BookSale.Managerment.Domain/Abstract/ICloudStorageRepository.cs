using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;

namespace BookSale.Managerment.Domain.Abstract
{
    public interface ICloudStorageRepository : IBaseRepository<CloudStorages>
    {
        Task<CloudStorages?> GetCloudStorageByTypeAsync(StorageType type);
        CloudStorages? GetCloudStorageByType(StorageType type);
    }
}