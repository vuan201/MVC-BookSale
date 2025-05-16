using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IGenreService
    {
        Task<ResponseModel<GenreDTO>> Create(GenreDTO genre);
        Task<ResponseModel<GenreDTO>> Delete(int id);
        Task<ResponseModel<IEnumerable<GenreDTO>>> GetGenreList(RequestFilterModel filter);
        Task<ResponseModel<GenreDTO>> Update(GenreDTO genre);
    }
}