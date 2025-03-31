using BookSale.Managerment.Domain.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookSale.Managerment.Application.DTOs;
namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Weakly typed: Kiểu không rõ ràng
            ViewBag.Error = "Error";
            ViewBag.success = "Success";
            ViewData["Message"] = "Hello";

            // Lưu trữ trong session, chỉ tồn tại 1 lần duy nhất
            TempData["Warning"] = "Warning";  // Có thể truyền qua nhiều controller khác nhau và nhiều request khác nhau

            var login = new ResponseModel(true, "Login Success");
            // Strongly type: Kiểu rõ ràng
            return View(login);

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
