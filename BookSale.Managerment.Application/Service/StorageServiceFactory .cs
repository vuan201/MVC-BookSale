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
                default:
                    throw new NotImplementedException($"Storage service type {storageServiceType} is not implemented.");
            }
        }
    }
}
