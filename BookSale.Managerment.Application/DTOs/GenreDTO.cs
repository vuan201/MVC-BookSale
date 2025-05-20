using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.DTOs
{
    public class GenreDTO
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id phải lớn hơn hoặc bằng 0")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
