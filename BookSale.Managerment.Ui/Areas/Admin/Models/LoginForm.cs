using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Ui.Areas.Admin.Models
{
    public class LoginForm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Mật khẩu phải tối thiểu 6 ký tự")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
