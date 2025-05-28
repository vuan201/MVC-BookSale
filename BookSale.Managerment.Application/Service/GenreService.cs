using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.constants;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Managerment.Application.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<GenreDTO>>> GetGenreList(RequestFilterModel filter)
        {
            var query = _unitOfWork.GenreRepository.AsQueryable();

            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(i => i.Name.Contains(filter.search));
            }

            var total = query.Count();

            var genres = await query.Skip(filter.Offset).Take(filter.Limit).ToListAsync();

            var listGenre = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            return new ResponseModel<IEnumerable<GenreDTO>>(true, ResponseMessage.GetDataSuccess,total, listGenre);
        }
        public async Task<ResponseModel<GenreDTO>> Create(GenreDTO genre)
        {
            if (genre is null || genre.Name is null) 
            {
                return new ResponseModel<GenreDTO>(false, ResponseMessage.InvalidValue);
            }

            var newGenre = new Genres();

            _mapper.Map(genre, newGenre);

            await _unitOfWork.GenreRepository.CreateAsync(newGenre);

            await _unitOfWork.SaveChangesAsync();

            _mapper.Map(newGenre, genre);

            return new ResponseModel<GenreDTO>(true, ResponseMessage.CreateSuccess, genre);
        }
        public async Task<ResponseModel<GenreDTO>> Update(GenreDTO genre)
        {
            if (genre is null || genre.Name is null)
            {
                return new ResponseModel<GenreDTO>(false, ResponseMessage.InvalidValue);
            }

            var oldGenre = await _unitOfWork.GenreRepository.GetByIdAsync(genre.Id);

            if (oldGenre is null) return new ResponseModel<GenreDTO>(false, ResponseMessage.DoesNotExist);

            _mapper.Map(genre, oldGenre);

            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<GenreDTO>(true, ResponseMessage.UpdateSuccess, genre);
        }
        public async Task<ResponseModel<GenreDTO>> Delete(int id)
        {
            var oldGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

            if (oldGenre is null) return new ResponseModel<GenreDTO>(false, ResponseMessage.DoesNotExist);

            _unitOfWork.GenreRepository.Delete(oldGenre);

            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<GenreDTO>(true, ResponseMessage.DeleteSuccess);
        }
    }
}
