using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managerment.Domain.Entity
{
    public class Files : EntityBase
    {
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Key { get; set; }
        public int CloudStorageId { get; set; }

        [ForeignKey(nameof(CloudStorageId))]
        public virtual CloudStorages CloudStorage { get; set; }
    }
}