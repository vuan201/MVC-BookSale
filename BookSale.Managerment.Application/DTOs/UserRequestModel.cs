using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Managerment.Application.DTOs
{
  public class UserRequestModel
  {
    public string? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập role")]
    public string RoleName { get; set;}

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [MinLength(6, ErrorMessage = "Tên đăng nhập phải có tối thiểu 6 ký tự")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
    [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt")]
    public string Password { get; set; }

    public IFormFile? Avata { get; set; }
  }
}