using BookSale.Managerment.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IStorageService
    {
        Task<ResponseModel<CloudinaryResponse>> UploadImage(IFormFile file, string fileName);
        string GetUrlImageByPublicId(string publicId);
    }
}