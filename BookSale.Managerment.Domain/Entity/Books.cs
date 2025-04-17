using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managerment.Domain.Entity
{
    public class Books : EntityBase
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual ApplicationUser Author { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Genres Genres { get; set; }
        public virtual ICollection<BookImages> BookImages { get; set; }
        public virtual ICollection<BookTags> BookTags { get; set; }
    }
}
