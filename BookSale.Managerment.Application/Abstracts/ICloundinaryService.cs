using BookSale.Managerment.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface ICloudinaryService
    {
        Task<ResponseModel> UploadImage(IFormFile file, string fileKey);
        string GetUrlImageByPublicId(string publicId);
    }
}