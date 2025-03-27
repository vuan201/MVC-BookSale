using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Ui.Areas.Admin.Models
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải tối thiểu 6 ký tự")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
