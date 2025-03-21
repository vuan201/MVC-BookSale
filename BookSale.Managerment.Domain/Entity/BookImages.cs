using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managerment.Domain.Entity
{
    public class BookImages
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Books Books { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }
    }
}
