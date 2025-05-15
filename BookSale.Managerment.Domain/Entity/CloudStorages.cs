using System.ComponentModel.DataAnnotations;
using BookSale.Managerment.Domain.Enums;

namespace BookSale.Managerment.Domain.Entity
{
    public class CloudStorages : EntityAdvancedManagement
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Account { get; set; }

        [StringLength(50)]
        public string? Key { get; set; }

        [StringLength(50)]
        public string? Secret { get; set; }
        public StorageType Type { get; set; }
    }
}