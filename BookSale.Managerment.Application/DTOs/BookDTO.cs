using BookSale.Managerment.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? GenreName { get; set; }
        public string? AuthorName { get; set; }
        public ICollection<TagDTO>? BookTags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
