using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Service
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenres();
    }
}