using BookSale.Managerment.Domain.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    public class HomeController : BaseControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
