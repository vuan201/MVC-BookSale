using BookSale.Managerment.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult AccountManagerment()
        {
            return View();
        }
        public async Task<IActionResult> GetListAccounts(RequestFilterModel filter)
        {
            return Json(1);
        }
        public IActionResult RoleManagerment()
        {
            return View();
        }
    }
}
