using BookSale.Managerment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.DTOs
{
    public class FIleDTO
    {
        public int Id { get; set; }
        public StorageType StorageType { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string? Url { get; set; }
        public FIleDTO(StorageType storageType, string name, string key) 
        {
            this.StorageType = storageType;
            this.Name = name;
            this.Key = key;
        }
    }
}
