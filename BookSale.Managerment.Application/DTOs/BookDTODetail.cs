using Microsoft.AspNetCore.Http;

namespace BookSale.Managerment.Application.DTOs
{
    public class BookDetailDTO : BookDTO
    {
        public int GenreId { get; set; }
        public string AuthorId { get; set; }
        public ICollection<IFormFile>? ImageFiles { get; set; }
        public IEnumerable<FIleDTO>? BookImages { get; set; }
    }
}
