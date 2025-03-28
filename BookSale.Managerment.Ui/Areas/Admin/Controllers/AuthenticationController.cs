using BookSale.Managerment.Application.Service;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Ui.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm loginForm)
        {
            /*
                - Nếu modelstate hợp lệ thì tiến hành kiểm tra username và password
                - Nếu đúng thì lưu thông tin vào session và chuyển hướng đến trang chủ 
                - Ngược lại thì hiển thị lỗi ra view
            */
            if (ModelState.IsValid)
            {
                // Gọi đến service để check login
                var (success, message) = await _userService.CheckLogin(loginForm.Username, loginForm.Password);

                if (!success)
                {
                    ViewBag.Error = message;
                    return View();
                }
                else
                {
                    // HttpContext.Session.SetString("Username", loginForm.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Lấy ra các lỗi của ModelState
                var errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage);

                ViewBag.errors = string.Join("<br/>", errors);
            }

            return View();
        }
    }
}
