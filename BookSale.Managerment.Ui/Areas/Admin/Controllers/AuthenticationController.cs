using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Ui.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseControl
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;         
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
                var Response = await _authenticationService.CheckLogin(loginForm.Username, loginForm.Password, loginForm.RememberMe);

                if (!Response.Status)
                {
                    ViewBag.Error = Response.Message;
                    return View();
                }
                else
                {
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
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();

            return Redirect(nameof(Login));
        }
    }
}
