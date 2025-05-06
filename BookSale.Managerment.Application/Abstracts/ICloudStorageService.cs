using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface ICloudStorageService
    {
        Task<CloudStorages?> GetStorageAsync(StorageType type);
        CloudStorages? GetStorage(StorageType type);
    }
}