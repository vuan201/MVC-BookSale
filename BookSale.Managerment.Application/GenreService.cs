using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            return (await _genreRepository.GetAllGenres()).Select(i => new GenreDTO { Id = i.Id, Name = i.Name, Description = i.Description });
        }
    }
}
