using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface ITagService
    {
        Task<ResponseModel<TagDTO>> Create(TagDTO genre);
        Task<ResponseModel<TagDTO>> Delete(int id);
        Task<ResponseModel<IEnumerable<TagDTO>>> GetTagList(RequestFilterModel filter);
        Task<ResponseModel<TagDTO>> Update(TagDTO genre);
    }
}