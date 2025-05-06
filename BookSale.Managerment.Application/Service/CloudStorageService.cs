using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CloudStorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CloudStorages?> GetStorageAsync(StorageType type)
        {
            return await _unitOfWork.CloudStorageRepository.GetCloudStorageByTypeAsync(type);
        }
        public CloudStorages? GetStorage(StorageType type)
        {
            return _unitOfWork.CloudStorageRepository.GetCloudStorageByType(type);
        }
    }
}
