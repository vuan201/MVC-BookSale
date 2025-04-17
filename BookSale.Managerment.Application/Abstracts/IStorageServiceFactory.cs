using BookSale.Managerment.Domain.Enums;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IStorageServiceFactory
    {
        IStorageService GetStorageService(StorageType storageServiceType);
    }
}