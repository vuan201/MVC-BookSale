using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Ui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(false, ResponseMessage.InvalidValue));
            }

            var result = genreDTO.Id == 0
                       ? await _genreService.Create(genreDTO)
                       : await _genreService.Update(genreDTO);

            if (result.Status) return Json(result);

            return BadRequest(result);
        }
    }
}
