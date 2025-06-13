using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.constants;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Managerment.Application.Service
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<TagDTO>>> GetTagList(RequestFilterModel filter)
        {
            var query = _unitOfWork.TagsRepository.AsQueryable();

            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(i => i.Name.Contains(filter.search));
            }

            var total = query.Count();

            var genres = await query.Skip(filter.Offset).Take(filter.Limit).ToListAsync();

            var listGenre = _mapper.Map<IEnumerable<TagDTO>>(genres);

            return new ResponseModel<IEnumerable<TagDTO>>(true, ResponseMessage.GetDataSuccess,total, listGenre);
        }
        public async Task<ResponseModel<List<TagDTO>>> GetAll()
        {
            var result = await _unitOfWork.TagsRepository.GetAllTag();

            return new ResponseModel<List<TagDTO>>(true, ResponseMessage.GetDataSuccess, _mapper.Map<List<TagDTO>>(result));
        }
        public async Task<ResponseModel<TagDTO>> Create(TagDTO genre)
        {
            if (genre is null || genre.Name is null) 
            {
                return new ResponseModel<TagDTO>(false, ResponseMessage.InvalidValue);
            }

            var newGenre = new Tags();

            _mapper.Map(genre, newGenre);

            await _unitOfWork.TagsRepository.CreateAsync(newGenre);

            await _unitOfWork.SaveChangesAsync();

            _mapper.Map(newGenre, genre);

            return new ResponseModel<TagDTO>(true, ResponseMessage.CreateSuccess, genre);
        }
        public async Task<ResponseModel<TagDTO>> Update(TagDTO genre)
        {
            if (genre is null || genre.Name is null)
            {
                return new ResponseModel<TagDTO>(false, ResponseMessage.InvalidValue);
            }

            var oldGenre = await _unitOfWork.TagsRepository.GetByIdAsync(genre.Id);

            if (oldGenre is null) return new ResponseModel<TagDTO>(false, ResponseMessage.DoesNotExist);

            _mapper.Map(genre, oldGenre);

            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<TagDTO>(true, ResponseMessage.UpdateSuccess, genre);
        }
        public async Task<ResponseModel<TagDTO>> Delete(int id)
        {
            var oldGenre = await _unitOfWork.TagsRepository.GetByIdAsync(id);

            if (oldGenre is null) return new ResponseModel<TagDTO>(false, ResponseMessage.DoesNotExist);

            _unitOfWork.TagsRepository.Delete(oldGenre);

            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<TagDTO>(true, ResponseMessage.DeleteSuccess);
        }
    }
}
