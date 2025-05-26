using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managerment.Domain.Entity
{
    public class Books : EntityAdvancedManagement
    {
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual ApplicationUser Author { get; set; }

        [ForeignKey(nameof(GenreId))]
        public virtual Genres Genres { get; set; }
        public virtual ICollection<BookImages> BookImages { get; set; }
        public virtual ICollection<BookTags> BookTags { get; set; }
    }
}
