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
    public class GenreController : BaseControl
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

            var result = await _genreService.GetGenreList(filter);

            if(result.Status == false) return BadRequest(result);

            return Json(new TableViewModel<GenreDTO>()
            {
                total = result.Total,
                Rows = result.Data?.ToList()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _genreService.Delete(id);

            if(result.Status)
                return Json(result);

            return BadRequest(result);
        }
    }
}
