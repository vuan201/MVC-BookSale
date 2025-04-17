using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public class StorageServiceFactory : IStorageServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public StorageServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStorageService GetStorageService(StorageType storageServiceType)
        {
            switch (storageServiceType)
            {
                case StorageType.Cloudinary:
                    return _serviceProvider.GetService<CloudinaryService>();
                // case StorageType.FirebaseStorage:
                //     return _serviceProvider.GetService<FirebaseStorageService>();
                // case StorageType.Azure:
                //     return _serviceProvider.GetService<AzureBlobStorageService>();
                // case StorageType.S3:
                //     return _serviceProvider.GetService<AwsS3StorageService>();
                default:
                    throw new NotImplementedException($"Storage service type {storageServiceType} is not implemented.");
            }
        }
    }
}
