using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task <IActionResult> Index()
        {
            var data = await _unitOfWork.GenreRepository.GetAllGenres();
            var viewData = data.Select(i => new GenreDTO{ Id = i.Id, Name = i.Name, Description = i.Description});
            return View(viewData);
        }
    }
}
