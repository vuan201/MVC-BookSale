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
    public class TagController : BaseControl
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> TagList(RequestFilterModel filter)
        {
            if (filter == null || filter.Limit <= 0 || filter.Offset < 0)
            {
                return BadRequest(new { message = "Params khôn hợp lệ!" });
            }

            var genres = await _tagService.GetTagList(filter);

            return Json(new TableViewModel<TagDTO>()
            {
                total = genres.Total,
                Rows = genres.Data?.ToList()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TagDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel(false, ResponseMessage.InvalidValue));
            }

            var result = genreDTO.Id == 0
                       ? await _tagService.Create(genreDTO)
                       : await _tagService.Update(genreDTO);

            if (result.Status) return Json(result);

            return BadRequest(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tagService.Delete(id);

            if(result.Status)
                return Json(result);

            return BadRequest(result);
        }
    }
}
