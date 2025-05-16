using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public  IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GenreList(RequestFilterModel filter)
        {
            if (filter == null || filter.Limit <= 0 || filter.Offset < 0)
            {
                return BadRequest(new { message = "Params khôn hợp lệ!" });
            }

            var genres = await _genreService.GetGenreList(filter);

            return Json(new TableViewModel<GenreDTO>()
            {
                total = genres.Total,
                Rows = genres.Data?.ToList()
            });
        }
    }
}
