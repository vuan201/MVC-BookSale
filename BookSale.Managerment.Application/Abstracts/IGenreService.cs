using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenres();
    }
}