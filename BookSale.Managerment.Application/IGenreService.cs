using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenres();
    }
}