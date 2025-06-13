using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    public class BookController : BaseControl
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public BookController(IBookService bookService, IGenreService genreService, ITagService tagService, IUserService userService)
        {
            _bookService = bookService;
            _genreService = genreService;
            _tagService = tagService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetBookList(RequestFilterModel filter)
        {
            if (filter == null || filter.Limit <= 0 || filter.Offset < 0)
            {
                return BadRequest(new { message = "Params khôn hợp lệ!" });
            }

            var result = await _bookService.GetBookList(filter);

            if (result.Status == false) return BadRequest(result);

            return Json(new TableViewModel<BookDTO>()
            {
                total = result.Total,
                Rows = result.Data?.ToList()
            });
        }
        public async Task<IActionResult> Save(int? id)
        {
            var genres = await _genreService.GetAll();
            var tags = await _tagService.GetAll();
            var authors = await _userService.GetAllAuthor();

            ViewBag.Genres = genres.Data ?? new List<GenreDTO>();
            ViewBag.Tags = tags.Data ?? new List<TagDTO>();
            ViewBag.Authors = authors.Data ?? new List<UserDto>();

            if(id is not null)
            {
                var result = await _bookService.GetBook(id.Value);

                if (result.Status == true && result.Data is not null) return View(result.Data);
                else return BadRequest(result);
            }
            return View(new BookDetailDTO());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(BookDetailDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = model.Id == 0 
                       ? await _bookService.Create(model)
                       : await _bookService.Update(model.Id, model);

            if(result.Status == false) return BadRequest(result);

            return View(result);
        }
    }
}
