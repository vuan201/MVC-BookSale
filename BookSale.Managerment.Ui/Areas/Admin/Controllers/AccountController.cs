using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult AccountManagerment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetListAccounts(RequestFilterModel filter)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(filter == null || filter.Limit <= 0 || filter.Offset < 0)
            {
                return BadRequest(new { message = "Params khôn hợp lệ!" });
            }

            var user = _userService.GetUsers(filter);

            return Json(user);
        }
        public IActionResult RoleManagerment()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(new UserRequestModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRequestModel model)
        {
            // Kiểm tra validation trước
            if (!ModelState.IsValid)
            {
                // Gửi lại model để giữ lỗi hiển thị
                return View(model);
            }

            // Gọi service để lưu dữ liệu
            var result = await _userService.Save(model);

            if (result.Status)
            {
                // Nếu thành công, chuyển hướng về trang quản lý tài khoản
                return Redirect(nameof(AccountManagerment));
            }

            // Nếu có lỗi từ service, thêm vào ModelState để hiển thị
            ModelState.AddModelError("ErrorMessage", result.Message);

            // Trả về view với model và thông báo lỗi
            return View(model);
        }
    }
}
