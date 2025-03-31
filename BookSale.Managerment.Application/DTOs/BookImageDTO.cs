using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.DTOs
{
    public class BookImageDTO
    {
        public BookImageDTO(int id, int bookId, string imageUrl)
        {
            Id = id;
            BookId = bookId;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public string ImageUrl { get; set; }
    }
}
