using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain.Enums;
using BookSale.Managerment.Ui.Models;
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
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IMapper mapper, IStorageServiceFactory storageServiceFactory)
        {
            _userService = userService;
            _mapper = mapper;
            _storageService = storageServiceFactory.GetStorageService(StorageType.Cloudinary);
        }

        public IActionResult AccountManagerment()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetListAccounts(RequestFilterModel filter)
        {
            if (filter == null || filter.Limit <= 0 || filter.Offset < 0)
            {
                return BadRequest(new { message = "Params khôn hợp lệ!" });
            }

            var result = _userService.GetUsers(filter);
            var viewModel = new TableViewModel<UserDto>
            {
                total = result.Total,
                Rows = result.Data,
            };
            return Json(viewModel);
        }
        public IActionResult RoleManagerment()
        {
            return View();
        }
        public async Task<IActionResult> Save(string? id)
        {
            if(!string.IsNullOrEmpty(id)) {
                var user = await _userService.GetUserById(id);
                return View(user);
            }

            return View(new UserDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserDto model)
        {
            // Kiểm tra validation trước
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();

                // Thêm lỗi vào ModelState
                ModelState.AddModelError("ErrorMessage", string.Join(", ", errors));

                // Gửi lại model để giữ lỗi hiển thị
                return View(model);
            }

            // Gọi service để lưu dữ liệu
            var result = string.IsNullOrEmpty(model.Id) 
                       ? await _userService.Create(model) 
                       : await _userService.Update(model);

            if (result.Status)
            {
                // Nếu thành công, chuyển hướng về trang quản lý tài khoản
                return RedirectToAction(nameof(AccountManagerment), "Account");
            }

            // Nếu có lỗi từ service, thêm vào ModelState để hiển thị
            ModelState.AddModelError("ErrorMessage", result.Message);

            // Trả về view với model và thông báo lỗi
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUser(id);
            if(result.Status)
                return Json(result);

            // Error
            return NotFound(result);
        }
    }
}
