using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllGenres();
            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }
    }
}
