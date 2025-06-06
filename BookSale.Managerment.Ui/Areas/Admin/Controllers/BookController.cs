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

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
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
            if(id is not null)
            {
                var result = await _bookService.GetBook(id.Value);

                if (result.Status == true && result.Data is not null) return View(result.Data);
                else return BadRequest(result);
            }
            return View(new BookDetailDTO());
        }
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

            return Json(result);
        }
    }
}
