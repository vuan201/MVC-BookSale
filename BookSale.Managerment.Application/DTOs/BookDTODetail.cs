using BookSale.Managerment.Domain.Extension;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
    public class BookDetailDTO
    {
        public int Id { get; set; }
        [DisplayName("Mã sách")]
        public string Code { get; set; }
        [DisplayName("Tên sách")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        [DisplayName("Giá nhập")]
        public decimal Price { get; set; }
        [DisplayName("Thể loại")]
        public string? GenreName { get; set; }
        [DisplayName("Tên tác giả")]
        public string? AuthorName { get; set; }
        [DisplayName("Thẻ")]
        public List<int>? BookTagIds { get; set; }
        public int GenreId { get; set; }
        public string AuthorId { get; set; }
        public ICollection<IFormFile>? ImageFiles { get; set; }
        public IEnumerable<FIleDTO>? BookImages { get; set; }
        public BookDetailDTO()
        {
            this.Code = UniqueCodeGenerator.GenerateUniqueGuid();
        }
    }
}
