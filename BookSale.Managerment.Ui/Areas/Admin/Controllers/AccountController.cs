using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var user = _userService.GetAllUsers(filter);

            return Json(user);
        }
        public IActionResult RoleManagerment()
        {
            return View();
        }
    }
}
