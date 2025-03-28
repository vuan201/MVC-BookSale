using BookSale.Managerment.Domain.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // public async Task<IActionResult> Index()
        // {
        //     var data = await _unitOfWork.GenreRepository.GetAllGenres();
        //     var viewData = data.Select(i => new GenreDTO{ Id = i.Id, Name = i.Name, Description = i.Description});
        //     return View(viewData);
        // }
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
